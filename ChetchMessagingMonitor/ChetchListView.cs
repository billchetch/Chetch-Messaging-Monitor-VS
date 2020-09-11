using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using Chetch.Utilities;

namespace ChetchMessagingMonitor
{
    public class ChetchListView : ListView
    {
        public class Filter
        {
            public ChetchListView CLV;
            public String PropertyName { get; internal set; }
            public Dictionary<Control, List<Object>> Controls = new Dictionary<Control, List<Object>>();

            public Filter(String propertyName)
            {
                PropertyName = propertyName;

            }

            private void FilterValueChanged(object sender, EventArgs e)
            {
                CLV.PopulateItems();
            }

            public void AddControl(Control control, List<Object> values)
            {
                Controls[control] = values == null ? new List<Object>() : values;

                if(control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndexChanged += FilterValueChanged;
                }
                else if(control is CheckBox)
                {
                    ((CheckBox)control).CheckedChanged += FilterValueChanged;
                }
                else if (control is ListView)
                {
                    ((ListView)control).SelectedIndexChanged += FilterValueChanged;
                }
                else if (control is TextBox)
                {
                    ((TextBox)control).KeyUp += FilterValueChanged;
                }
            }

            public void AddControl(Control control, String values)
            {
                AddControl(control, values.Split(',').ToList<Object>());
            }

            public List<Control> GetControls()
            {
                return Controls.Keys.ToList();
            }

            public List<T> GetAllValues<T>()
            {
                List<T> values = new List<T>();
                foreach(var vals in Controls.Values)
                {
                    foreach (var v in vals)
                    {
                        values.Add((T)v);
                    }
                }
                return values;
            }

            public List<Object> GetAllValues()
            {
                return GetAllValues<Object>();
            }

            public bool Matches(DataSourceObject data)
            {
                if (data == null) throw new Exception("Cannot match on null data");

                String[] propertyNames = PropertyName.Split('|');
                foreach (var pName in propertyNames)
                {
                    foreach (var kv in Controls)
                    {
                        Control c = kv.Key;
                        List<Object> values = kv.Value;

                        if (c is TextBox)
                        {
                            var s1 = ((TextBox)c).Text;
                            if (s1 == null || s1 == String.Empty) return true;

                            s1 = s1.ToLower();
                            var s2 = data.Get<String>(pName).ToLower();
                            if (s2.IndexOf(s1) >= 0)
                            {
                                return true;
                            }
                        }
                        else if (c is CheckBox)
                        {
                            if (((CheckBox)c).Checked && values.Contains(data.Get<Object>(pName)))
                            {
                                return true;
                            }
                        }
                        else if (c is ComboBox)
                        {
                            var cmb = (ComboBox)c;
                            if (cmb.SelectedIndex == -1 || cmb.SelectedItem == null || ((String)cmb.SelectedItem) == String.Empty || values.Contains((String)cmb.SelectedItem))
                            {
                                return true;
                            }

                            var val = data.Get<Object>(pName);
                            if (val.ToString() == ((String)cmb.SelectedItem))
                            {
                                return true;
                            }
                        }
                        else if (c is ListView)
                        {
                            var lv = (ListView)c;
                            if (lv.SelectedItems == null || lv.SelectedItems.Count == 0)
                            {
                                return true;
                            }

                            var val = data.Get<String>(pName);
                            foreach (ListViewItem lvi in lv.SelectedItems)
                            {
                                if (lvi.Name == val) return true;
                            }
                        }
                        else
                        {
                            throw new Exception("ChetchListView::MatchesFilter does not support control " + c.GetType().ToString());
                        }
                    }
                }
                return false;
            }
        }

        private IBindingList _itemsSource;
        public IBindingList ItemsSource
        {
            get
            {
                return _itemsSource;
            }

            set
            {
                if(_itemsSource != null)
                {
                    _itemsSource.ListChanged -= HandleItemsSourceChanged;
                }
                _itemsSource = value;
                if (_itemsSource != null)
                {
                    _itemsSource.ListChanged += HandleItemsSourceChanged;
                }
            }
        }

        private Dictionary<String, Filter> _filters = new Dictionary<String, Filter>();

        public bool PrependItems { get; set; } = false;
        public String DataSourceObjectIDName { get; set; } = "ID";

        public bool Trace = true;
        
        private List<DataSourceObject> _filteredSource = new List<DataSourceObject>();
        protected List<DataSourceObject> FilteredSource
        {
            get
            {
                _filteredSource.Clear();
                foreach (DataSourceObject d in _itemsSource)
                {
                    if (IncludeInView(d))
                    {
                        _filteredSource.Add(d);
                    }
                }
                return _filteredSource;
            }
        }

        virtual protected void HandleItemsSourceChanged(object sender, ListChangedEventArgs e)
        {
            DataSourceObject data;

            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    data = (DataSourceObject)_itemsSource[e.NewIndex];
                    if (Trace) System.Diagnostics.Trace.WriteLine(String.Format("{0}: New data added to source (count = {1}) so adding item to list view (count = {2})", Name, _itemsSource.Count, Items.Count));
                    InvokeAction<DataSourceObject>(AddItem, data);
                    break;

                case ListChangedType.ItemDeleted:
                    data = (DataSourceObject)_itemsSource[e.NewIndex];
                    InvokeAction<DataSourceObject>(DeleteItem, data);
                    break;

                case ListChangedType.ItemMoved:
                    break;

                case ListChangedType.ItemChanged:
                    data = (DataSourceObject)_itemsSource[e.NewIndex];
                    InvokeAction<DataSourceObject>(UpdateItem, data);
                    if(Trace)System.Diagnostics.Trace.WriteLine(String.Format("{0}: Existing data updated in source (count = {1}) so updating item to list view (count = {2})", Name, _itemsSource.Count, Items.Count));
                    break;
            }
        }

        public void AddFilter(String propertyName, Control control, List<Object> rangeOfValues = null)
        {
            if (_filters.ContainsKey(propertyName))
            {
                _filters[propertyName].AddControl(control, rangeOfValues);
            }
            else
            {
                var f = new Filter(propertyName);
                f.CLV = this;
                f.AddControl(control, rangeOfValues);
                _filters[propertyName] = f;
            }
        }

        public void AddFilter(String propertyName, Control control, String values)
        {
            AddFilter(propertyName, control, values.Split(',').ToList<Object>());
        }

        public void AddFilter(String propertyName, Control control, Object[] values)
        {
            AddFilter(propertyName, control, values.ToList());
        }

        public Filter GetFilter(String propertyName)
        {
            return _filters.ContainsKey(propertyName) ? _filters[propertyName] : null;
        }

        public bool IncludeInView(DataSourceObject data)
        {
            foreach (var f in _filters.Values)
            {
                if (!f.Matches(data)) return false;
            }
            return true;
        }

        private void InvokeAction(Action action)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    action();
                });
            }
            else
            {
                action();
            }
        }

        private void InvokeAction<T>(Action<T> action, T t)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    action(t);
                });
            }
            else
            {
                action(t);
            }
        }

        virtual protected ListViewItem CreateItem(DataSourceObject data)
        {
            List<String> subItems = new List<String>();
            foreach(ColumnHeader col in Columns)
            {
                var pName = col.Tag.ToString();
                if (pName == null || pName == String.Empty || !data.HasProperty(pName))
                {
                    throw new Exception(String.Format("Cannot find property for column {0} using property name {1}", col.Name, pName));
                }
                var val = data.Get<Object>(pName);
                subItems.Add(val == null ? String.Empty : val.ToString());
            }

            ListViewItem lvi = new ListViewItem(subItems.ToArray());
            lvi.Name = GetID(data);
            return lvi;
        }

        protected String GetID(DataSourceObject data)
        {
            return data.Get<String>(DataSourceObjectIDName);
        }

        public void ClearItems(bool clearItemsSource = false)
        {
            if (clearItemsSource)
            {
                IBindingList temp = ItemsSource;
                ItemsSource = null;
                Items.Clear();
                temp.Clear();
                ItemsSource = temp;
            } else
            {
                Items.Clear();
            }
        }

        public void PopulateItems()
        {
            Items.Clear();
            foreach (var d in FilteredSource)
            {
                ListViewItem li = CreateItem(d);
                if (PrependItems)
                {
                    Items.Insert(0, li);
                }
                else
                {
                    Items.Add(li);
                }
            }
        }

        public void AddItem(DataSourceObject data)
        {
            if (!IncludeInView(data)) return;

            if (PrependItems)
            {
                Items.Insert(0, CreateItem(data));
            } else
            {
                Items.Add(CreateItem(data));
            }
        }

        public void UpdateItem(DataSourceObject data)
        {
            if (!IncludeInView(data)) return;

            int idx = Items.IndexOfKey(GetID(data));
            if (idx >= 0)
            {
                Items[idx] = CreateItem(data);
            }
        }

        public void DeleteItem(DataSourceObject data)
        {
            if (!IncludeInView(data)) return;

            int idx = Items.IndexOfKey(GetID(data));
            if (idx >= 0)
            {
                Items.RemoveAt(idx);    
            }
        }
    } //end class
}