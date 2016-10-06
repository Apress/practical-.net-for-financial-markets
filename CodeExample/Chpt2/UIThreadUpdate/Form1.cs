using System;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace UIThreadUpdate
{
	public delegate void SetProgressBar(int value);
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnUpload;
		private System.Windows.Forms.ProgressBar uploadProgressBar;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.uploadProgressBar = new System.Windows.Forms.ProgressBar();
			this.btnUpload = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// uploadProgressBar
			// 
			this.uploadProgressBar.Location = new System.Drawing.Point(8, 24);
			this.uploadProgressBar.Maximum = 10500;
			this.uploadProgressBar.Name = "uploadProgressBar";
			this.uploadProgressBar.Size = new System.Drawing.Size(240, 24);
			this.uploadProgressBar.TabIndex = 0;
			this.uploadProgressBar.Value = 60;
			// 
			// btnUpload
			// 
			this.btnUpload.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnUpload.Location = new System.Drawing.Point(120, 64);
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Size = new System.Drawing.Size(64, 24);
			this.btnUpload.TabIndex = 2;
			this.btnUpload.Text = "Upload";
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(192, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 24);
			this.button1.TabIndex = 3;
			this.button1.Text = "Cancel";
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(8, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(248, 8);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 16);
			this.label1.TabIndex = 5;
			this.label1.Text = "Uploading Order 5060 of 10500";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(266, 96);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnUpload);
			this.Controls.Add(this.uploadProgressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Form1";
			this.Text = "Bulk Order Upload";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void btnUpload_Click(object sender, System.EventArgs e)
		{
			//Starts the bulk order upload on worker thread
			ThreadPool.QueueUserWorkItem(new WaitCallback(BulkOrderUpload));			
		}
		private void UpdateProgressBar(int current)
		{
			//update the progress bar control
			uploadProgressBar.Value = current;
		}

		private void BulkOrderUpload(object state)
		{
			int ctr=0;
			int totalRecords=10500;

			//Read bulk order import file and initialize the values
			//such as total number of orders to import
			
			//start iterating individual order
			while(ctr < totalRecords)
			{
				//update progress bar control value
				//this needs to be done on UI thread
				uploadProgressBar.Invoke(new SetProgressBar(UpdateProgressBar),
					                     new object[]{ctr});
				ctr++;
			}

		}
	}
}
