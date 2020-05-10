namespace ChetchMessagingMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabClients = new System.Windows.Forms.TabPage();
            this.gbMessageFilters = new System.Windows.Forms.GroupBox();
            this.cmbFilterMessageDirection = new System.Windows.Forms.ComboBox();
            this.listViewClients = new ChetchMessagingMonitor.ChetchListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chContext = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessagesRecieved = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGarbageReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMessagesSent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabServer = new System.Windows.Forms.TabPage();
            this.cbMessageTypeStatus = new System.Windows.Forms.CheckBox();
            this.cbMessageTypePing = new System.Windows.Forms.CheckBox();
            this.cbMessageTypeError = new System.Windows.Forms.CheckBox();
            this.cbMessageTypeCommand = new System.Windows.Forms.CheckBox();
            this.cbMessageTypeOther = new System.Windows.Forms.CheckBox();
            this.listViewMessages = new ChetchMessagingMonitor.ChetchListView();
            this.mhID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mhDirection = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mhTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mhSender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mhSummary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbMessageDetails = new System.Windows.Forms.TextBox();
            this.cmbSendType = new System.Windows.Forms.ComboBox();
            this.tbCommandLine = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnSendPing = new System.Windows.Forms.Button();
            this.btnSendStatusRequest = new System.Windows.Forms.Button();
            this.listViewServerConnections = new ChetchMessagingMonitor.ChetchListView();
            this.schID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.schType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.schName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.schState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.schExtras = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbServerDetails = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabClients.SuspendLayout();
            this.gbMessageFilters.SuspendLayout();
            this.tabServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabClients);
            this.tabControl1.Controls.Add(this.tabServer);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(883, 618);
            this.tabControl1.TabIndex = 0;
            // 
            // tabClients
            // 
            this.tabClients.Controls.Add(this.btnSendStatusRequest);
            this.tabClients.Controls.Add(this.btnSendPing);
            this.tabClients.Controls.Add(this.btnSend);
            this.tabClients.Controls.Add(this.tbCommandLine);
            this.tabClients.Controls.Add(this.cmbSendType);
            this.tabClients.Controls.Add(this.tbMessageDetails);
            this.tabClients.Controls.Add(this.listViewMessages);
            this.tabClients.Controls.Add(this.gbMessageFilters);
            this.tabClients.Controls.Add(this.listViewClients);
            this.tabClients.Location = new System.Drawing.Point(4, 22);
            this.tabClients.Name = "tabClients";
            this.tabClients.Padding = new System.Windows.Forms.Padding(3);
            this.tabClients.Size = new System.Drawing.Size(875, 592);
            this.tabClients.TabIndex = 0;
            this.tabClients.Text = "Clients";
            this.tabClients.UseVisualStyleBackColor = true;
            // 
            // gbMessageFilters
            // 
            this.gbMessageFilters.Controls.Add(this.cbMessageTypeOther);
            this.gbMessageFilters.Controls.Add(this.cbMessageTypeError);
            this.gbMessageFilters.Controls.Add(this.cbMessageTypeCommand);
            this.gbMessageFilters.Controls.Add(this.cbMessageTypePing);
            this.gbMessageFilters.Controls.Add(this.cbMessageTypeStatus);
            this.gbMessageFilters.Controls.Add(this.cmbFilterMessageDirection);
            this.gbMessageFilters.Location = new System.Drawing.Point(3, 191);
            this.gbMessageFilters.Name = "gbMessageFilters";
            this.gbMessageFilters.Size = new System.Drawing.Size(866, 42);
            this.gbMessageFilters.TabIndex = 1;
            this.gbMessageFilters.TabStop = false;
            // 
            // cmbFilterMessageDirection
            // 
            this.cmbFilterMessageDirection.FormattingEnabled = true;
            this.cmbFilterMessageDirection.Location = new System.Drawing.Point(7, 13);
            this.cmbFilterMessageDirection.Name = "cmbFilterMessageDirection";
            this.cmbFilterMessageDirection.Size = new System.Drawing.Size(121, 21);
            this.cmbFilterMessageDirection.TabIndex = 0;
            // 
            // listViewClients
            // 
            this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chName,
            this.chState,
            this.chContext,
            this.chMessagesRecieved,
            this.chGarbageReceived,
            this.chMessagesSent});
            this.listViewClients.DataSourceObjectIDName = "Name";
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.ItemsSource = null;
            this.listViewClients.Location = new System.Drawing.Point(3, 7);
            this.listViewClients.MultiSelect = false;
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.PrependItems = false;
            this.listViewClients.Size = new System.Drawing.Size(866, 177);
            this.listViewClients.TabIndex = 0;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.Details;
            // 
            // chID
            // 
            this.chID.Tag = "ID";
            this.chID.Text = "ID";
            this.chID.Width = 149;
            // 
            // chName
            // 
            this.chName.Tag = "Name";
            this.chName.Text = "Name";
            this.chName.Width = 168;
            // 
            // chState
            // 
            this.chState.Tag = "State";
            this.chState.Text = "State";
            this.chState.Width = 114;
            // 
            // chContext
            // 
            this.chContext.Tag = "Context";
            this.chContext.Text = "Context";
            this.chContext.Width = 106;
            // 
            // chMessagesRecieved
            // 
            this.chMessagesRecieved.Tag = "MessagesReceived";
            this.chMessagesRecieved.Text = "Received";
            this.chMessagesRecieved.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chMessagesRecieved.Width = 67;
            // 
            // chGarbageReceived
            // 
            this.chGarbageReceived.Tag = "GarbageReceived";
            this.chGarbageReceived.Text = "Garbage";
            this.chGarbageReceived.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chGarbageReceived.Width = 65;
            // 
            // chMessagesSent
            // 
            this.chMessagesSent.Tag = "MessagesSent";
            this.chMessagesSent.Text = "Sent";
            this.chMessagesSent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chMessagesSent.Width = 64;
            // 
            // tabServer
            // 
            this.tabServer.Controls.Add(this.tbServerDetails);
            this.tabServer.Controls.Add(this.listViewServerConnections);
            this.tabServer.Location = new System.Drawing.Point(4, 22);
            this.tabServer.Name = "tabServer";
            this.tabServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabServer.Size = new System.Drawing.Size(875, 592);
            this.tabServer.TabIndex = 1;
            this.tabServer.Text = "Server";
            this.tabServer.UseVisualStyleBackColor = true;
            // 
            // cbMessageTypeStatus
            // 
            this.cbMessageTypeStatus.AutoSize = true;
            this.cbMessageTypeStatus.Checked = true;
            this.cbMessageTypeStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMessageTypeStatus.Location = new System.Drawing.Point(157, 16);
            this.cbMessageTypeStatus.Name = "cbMessageTypeStatus";
            this.cbMessageTypeStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbMessageTypeStatus.Size = new System.Drawing.Size(56, 17);
            this.cbMessageTypeStatus.TabIndex = 1;
            this.cbMessageTypeStatus.Text = "Status";
            this.cbMessageTypeStatus.UseVisualStyleBackColor = true;
            // 
            // cbMessageTypePing
            // 
            this.cbMessageTypePing.AutoSize = true;
            this.cbMessageTypePing.Checked = true;
            this.cbMessageTypePing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMessageTypePing.Location = new System.Drawing.Point(221, 16);
            this.cbMessageTypePing.Name = "cbMessageTypePing";
            this.cbMessageTypePing.Size = new System.Drawing.Size(47, 17);
            this.cbMessageTypePing.TabIndex = 2;
            this.cbMessageTypePing.Text = "Ping";
            this.cbMessageTypePing.UseVisualStyleBackColor = true;
            // 
            // cbMessageTypeError
            // 
            this.cbMessageTypeError.AutoSize = true;
            this.cbMessageTypeError.Checked = true;
            this.cbMessageTypeError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMessageTypeError.Location = new System.Drawing.Point(362, 16);
            this.cbMessageTypeError.Name = "cbMessageTypeError";
            this.cbMessageTypeError.Size = new System.Drawing.Size(48, 17);
            this.cbMessageTypeError.TabIndex = 4;
            this.cbMessageTypeError.Text = "Error";
            this.cbMessageTypeError.UseVisualStyleBackColor = true;
            // 
            // cbMessageTypeCommand
            // 
            this.cbMessageTypeCommand.AutoSize = true;
            this.cbMessageTypeCommand.Checked = true;
            this.cbMessageTypeCommand.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMessageTypeCommand.Location = new System.Drawing.Point(276, 16);
            this.cbMessageTypeCommand.Name = "cbMessageTypeCommand";
            this.cbMessageTypeCommand.Size = new System.Drawing.Size(73, 17);
            this.cbMessageTypeCommand.TabIndex = 3;
            this.cbMessageTypeCommand.Text = "Command";
            this.cbMessageTypeCommand.UseVisualStyleBackColor = true;
            // 
            // cbMessageTypeOther
            // 
            this.cbMessageTypeOther.AutoSize = true;
            this.cbMessageTypeOther.Checked = true;
            this.cbMessageTypeOther.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMessageTypeOther.Location = new System.Drawing.Point(419, 16);
            this.cbMessageTypeOther.Name = "cbMessageTypeOther";
            this.cbMessageTypeOther.Size = new System.Drawing.Size(52, 17);
            this.cbMessageTypeOther.TabIndex = 5;
            this.cbMessageTypeOther.Text = "Other";
            this.cbMessageTypeOther.UseVisualStyleBackColor = true;
            // 
            // listViewMessages
            // 
            this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mhID,
            this.mhDirection,
            this.mhType,
            this.mhTarget,
            this.mhSender,
            this.mhSummary});
            this.listViewMessages.DataSourceObjectIDName = "ID";
            this.listViewMessages.FullRowSelect = true;
            this.listViewMessages.ItemsSource = null;
            this.listViewMessages.Location = new System.Drawing.Point(3, 240);
            this.listViewMessages.MultiSelect = false;
            this.listViewMessages.Name = "listViewMessages";
            this.listViewMessages.PrependItems = false;
            this.listViewMessages.Size = new System.Drawing.Size(866, 170);
            this.listViewMessages.TabIndex = 2;
            this.listViewMessages.UseCompatibleStateImageBehavior = false;
            this.listViewMessages.View = System.Windows.Forms.View.Details;
            // 
            // mhID
            // 
            this.mhID.Tag = "ID";
            this.mhID.Text = "ID";
            this.mhID.Width = 84;
            // 
            // mhDirection
            // 
            this.mhDirection.Tag = "Direction";
            this.mhDirection.Text = "Direction";
            this.mhDirection.Width = 87;
            // 
            // mhType
            // 
            this.mhType.Tag = "Type";
            this.mhType.Text = "Type";
            this.mhType.Width = 147;
            // 
            // mhTarget
            // 
            this.mhTarget.Tag = "Target";
            this.mhTarget.Text = "Target";
            this.mhTarget.Width = 99;
            // 
            // mhSender
            // 
            this.mhSender.Tag = "Sender";
            this.mhSender.Text = "Sender";
            this.mhSender.Width = 96;
            // 
            // mhSummary
            // 
            this.mhSummary.Tag = "Summary";
            this.mhSummary.Text = "Summary";
            this.mhSummary.Width = 323;
            // 
            // tbMessageDetails
            // 
            this.tbMessageDetails.Enabled = false;
            this.tbMessageDetails.Location = new System.Drawing.Point(3, 417);
            this.tbMessageDetails.Multiline = true;
            this.tbMessageDetails.Name = "tbMessageDetails";
            this.tbMessageDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessageDetails.Size = new System.Drawing.Size(495, 169);
            this.tbMessageDetails.TabIndex = 3;
            // 
            // cmbSendType
            // 
            this.cmbSendType.FormattingEnabled = true;
            this.cmbSendType.Items.AddRange(new object[] {
            "Command",
            "Raw Text"});
            this.cmbSendType.Location = new System.Drawing.Point(504, 416);
            this.cmbSendType.Name = "cmbSendType";
            this.cmbSendType.Size = new System.Drawing.Size(91, 21);
            this.cmbSendType.TabIndex = 4;
            // 
            // tbCommandLine
            // 
            this.tbCommandLine.Location = new System.Drawing.Point(601, 416);
            this.tbCommandLine.Name = "tbCommandLine";
            this.tbCommandLine.Size = new System.Drawing.Size(176, 20);
            this.tbCommandLine.TabIndex = 5;
            this.tbCommandLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommandLine_KeyPress);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(783, 415);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(86, 23);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnSendPing
            // 
            this.btnSendPing.Location = new System.Drawing.Point(504, 443);
            this.btnSendPing.Name = "btnSendPing";
            this.btnSendPing.Size = new System.Drawing.Size(91, 23);
            this.btnSendPing.TabIndex = 7;
            this.btnSendPing.Text = "Ping";
            this.btnSendPing.UseVisualStyleBackColor = true;
            this.btnSendPing.Click += new System.EventHandler(this.btnSendPing_Click);
            // 
            // btnSendStatusRequest
            // 
            this.btnSendStatusRequest.Location = new System.Drawing.Point(504, 472);
            this.btnSendStatusRequest.Name = "btnSendStatusRequest";
            this.btnSendStatusRequest.Size = new System.Drawing.Size(91, 23);
            this.btnSendStatusRequest.TabIndex = 8;
            this.btnSendStatusRequest.Text = "Status";
            this.btnSendStatusRequest.UseVisualStyleBackColor = true;
            this.btnSendStatusRequest.Click += new System.EventHandler(this.btnSendStatusRequest_Click);
            // 
            // listViewServerConnections
            // 
            this.listViewServerConnections.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.schID,
            this.schType,
            this.schName,
            this.schState,
            this.schExtras});
            this.listViewServerConnections.DataSourceObjectIDName = "ID";
            this.listViewServerConnections.FullRowSelect = true;
            this.listViewServerConnections.ItemsSource = null;
            this.listViewServerConnections.Location = new System.Drawing.Point(4, 34);
            this.listViewServerConnections.MultiSelect = false;
            this.listViewServerConnections.Name = "listViewServerConnections";
            this.listViewServerConnections.PrependItems = false;
            this.listViewServerConnections.Size = new System.Drawing.Size(865, 167);
            this.listViewServerConnections.TabIndex = 0;
            this.listViewServerConnections.UseCompatibleStateImageBehavior = false;
            this.listViewServerConnections.View = System.Windows.Forms.View.Details;
            // 
            // schID
            // 
            this.schID.Tag = "ID";
            this.schID.Text = "ID";
            this.schID.Width = 101;
            // 
            // schType
            // 
            this.schType.Tag = "ConnectionType";
            this.schType.Text = "Type";
            this.schType.Width = 97;
            // 
            // schName
            // 
            this.schName.Tag = "Name";
            this.schName.Text = "Name";
            this.schName.Width = 122;
            // 
            // schState
            // 
            this.schState.Tag = "State";
            this.schState.Text = "State";
            this.schState.Width = 108;
            // 
            // schExtras
            // 
            this.schExtras.Tag = "Extras";
            this.schExtras.Text = "Extras";
            this.schExtras.Width = 412;
            // 
            // tbServerDetails
            // 
            this.tbServerDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbServerDetails.Enabled = false;
            this.tbServerDetails.Location = new System.Drawing.Point(4, 6);
            this.tbServerDetails.Name = "tbServerDetails";
            this.tbServerDetails.Size = new System.Drawing.Size(433, 13);
            this.tbServerDetails.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 643);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "Chetch Messaging Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabClients.ResumeLayout(false);
            this.tabClients.PerformLayout();
            this.gbMessageFilters.ResumeLayout(false);
            this.gbMessageFilters.PerformLayout();
            this.tabServer.ResumeLayout(false);
            this.tabServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabClients;
        private ChetchListView listViewClients;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chState;
        private System.Windows.Forms.ColumnHeader chContext;
        private System.Windows.Forms.ColumnHeader chMessagesRecieved;
        private System.Windows.Forms.ColumnHeader chGarbageReceived;
        private System.Windows.Forms.ColumnHeader chMessagesSent;
        private System.Windows.Forms.TabPage tabServer;
        private System.Windows.Forms.GroupBox gbMessageFilters;
        private System.Windows.Forms.ComboBox cmbFilterMessageDirection;
        private System.Windows.Forms.CheckBox cbMessageTypeOther;
        private System.Windows.Forms.CheckBox cbMessageTypeError;
        private System.Windows.Forms.CheckBox cbMessageTypeCommand;
        private System.Windows.Forms.CheckBox cbMessageTypePing;
        private System.Windows.Forms.CheckBox cbMessageTypeStatus;
        private ChetchListView listViewMessages;
        private System.Windows.Forms.ColumnHeader mhID;
        private System.Windows.Forms.ColumnHeader mhDirection;
        private System.Windows.Forms.ColumnHeader mhType;
        private System.Windows.Forms.ColumnHeader mhTarget;
        private System.Windows.Forms.ColumnHeader mhSender;
        private System.Windows.Forms.ColumnHeader mhSummary;
        private System.Windows.Forms.TextBox tbMessageDetails;
        private System.Windows.Forms.ComboBox cmbSendType;
        private System.Windows.Forms.TextBox tbCommandLine;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnSendStatusRequest;
        private System.Windows.Forms.Button btnSendPing;
        private ChetchListView listViewServerConnections;
        private System.Windows.Forms.ColumnHeader schID;
        private System.Windows.Forms.ColumnHeader schType;
        private System.Windows.Forms.ColumnHeader schName;
        private System.Windows.Forms.ColumnHeader schState;
        private System.Windows.Forms.ColumnHeader schExtras;
        private System.Windows.Forms.TextBox tbServerDetails;
    }
}

