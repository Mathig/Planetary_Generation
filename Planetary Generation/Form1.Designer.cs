namespace Planetary_Generation
{
    partial class Form1
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.sizeInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label0 = new System.Windows.Forms.Label();
            this.directoryInput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.heightMapInput = new System.Windows.Forms.TextBox();
            this.plateMapInput = new System.Windows.Forms.TextBox();
            this.timeStepInput = new System.Windows.Forms.TextBox();
            this.velocityInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.movePlateButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outNameInput = new System.Windows.Forms.TextBox();
            this.genParamsInput = new System.Windows.Forms.TextBox();
            this.cutoffInput = new System.Windows.Forms.TextBox();
            this.numberOfPlatesInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.generationButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.errorDisplay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(340, 308);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // sizeInput
            // 
            this.sizeInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sizeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizeInput.Location = new System.Drawing.Point(830, 360);
            this.sizeInput.Name = "sizeInput";
            this.sizeInput.Size = new System.Drawing.Size(110, 26);
            this.sizeInput.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(826, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Size";
            // 
            // label0
            // 
            this.label0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label0.AutoSize = true;
            this.label0.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label0.Location = new System.Drawing.Point(339, 337);
            this.label0.Name = "label0";
            this.label0.Size = new System.Drawing.Size(72, 20);
            this.label0.TabIndex = 14;
            this.label0.Text = "Directory";
            // 
            // directoryInput
            // 
            this.directoryInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryInput.Location = new System.Drawing.Point(339, 360);
            this.directoryInput.Name = "directoryInput";
            this.directoryInput.Size = new System.Drawing.Size(485, 26);
            this.directoryInput.TabIndex = 15;
            this.directoryInput.TextChanged += new System.EventHandler(this.directoryInput_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.heightMapInput);
            this.tabPage2.Controls.Add(this.plateMapInput);
            this.tabPage2.Controls.Add(this.timeStepInput);
            this.tabPage2.Controls.Add(this.velocityInput);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.movePlateButton);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(597, 110);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plate Movement";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // heightMapInput
            // 
            this.heightMapInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.heightMapInput.Location = new System.Drawing.Point(278, 78);
            this.heightMapInput.Name = "heightMapInput";
            this.heightMapInput.Size = new System.Drawing.Size(153, 26);
            this.heightMapInput.TabIndex = 11;
            // 
            // plateMapInput
            // 
            this.plateMapInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plateMapInput.Location = new System.Drawing.Point(119, 78);
            this.plateMapInput.Name = "plateMapInput";
            this.plateMapInput.Size = new System.Drawing.Size(153, 26);
            this.plateMapInput.TabIndex = 9;
            // 
            // timeStepInput
            // 
            this.timeStepInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeStepInput.Location = new System.Drawing.Point(3, 78);
            this.timeStepInput.Name = "timeStepInput";
            this.timeStepInput.Size = new System.Drawing.Size(110, 26);
            this.timeStepInput.TabIndex = 4;
            // 
            // velocityInput
            // 
            this.velocityInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.velocityInput.Location = new System.Drawing.Point(3, 26);
            this.velocityInput.Name = "velocityInput";
            this.velocityInput.Size = new System.Drawing.Size(587, 26);
            this.velocityInput.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(274, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "Height Map";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(115, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Plate Map";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Time Step";
            // 
            // movePlateButton
            // 
            this.movePlateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.movePlateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.movePlateButton.Location = new System.Drawing.Point(438, 55);
            this.movePlateButton.Name = "movePlateButton";
            this.movePlateButton.Size = new System.Drawing.Size(153, 49);
            this.movePlateButton.TabIndex = 2;
            this.movePlateButton.Text = "Move Plates";
            this.movePlateButton.UseVisualStyleBackColor = true;
            this.movePlateButton.Click += new System.EventHandler(this.MovePlateButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Velocity";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(339, 390);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(605, 143);
            this.tabControl.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.outNameInput);
            this.tabPage1.Controls.Add(this.genParamsInput);
            this.tabPage1.Controls.Add(this.cutoffInput);
            this.tabPage1.Controls.Add(this.numberOfPlatesInput);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.generationButton);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(597, 110);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Plate Generation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // outNameInput
            // 
            this.outNameInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outNameInput.Location = new System.Drawing.Point(235, 78);
            this.outNameInput.Name = "outNameInput";
            this.outNameInput.Size = new System.Drawing.Size(194, 26);
            this.outNameInput.TabIndex = 17;
            // 
            // genParamsInput
            // 
            this.genParamsInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genParamsInput.Location = new System.Drawing.Point(3, 26);
            this.genParamsInput.Name = "genParamsInput";
            this.genParamsInput.Size = new System.Drawing.Size(587, 26);
            this.genParamsInput.TabIndex = 9;
            // 
            // cutoffInput
            // 
            this.cutoffInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cutoffInput.Location = new System.Drawing.Point(3, 78);
            this.cutoffInput.Name = "cutoffInput";
            this.cutoffInput.Size = new System.Drawing.Size(110, 26);
            this.cutoffInput.TabIndex = 12;
            // 
            // numberOfPlatesInput
            // 
            this.numberOfPlatesInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfPlatesInput.Location = new System.Drawing.Point(119, 78);
            this.numberOfPlatesInput.Name = "numberOfPlatesInput";
            this.numberOfPlatesInput.Size = new System.Drawing.Size(110, 26);
            this.numberOfPlatesInput.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(231, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "File Name";
            // 
            // generationButton
            // 
            this.generationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.generationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generationButton.Location = new System.Drawing.Point(438, 55);
            this.generationButton.Name = "generationButton";
            this.generationButton.Size = new System.Drawing.Size(153, 49);
            this.generationButton.TabIndex = 0;
            this.generationButton.Text = "Run Generation";
            this.generationButton.UseVisualStyleBackColor = true;
            this.generationButton.Click += new System.EventHandler(this.GenerationButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Generation Parameters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Cutoff";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(115, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Plate Count";
            // 
            // errorDisplay
            // 
            this.errorDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.errorDisplay.AutoSize = true;
            this.errorDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorDisplay.Location = new System.Drawing.Point(342, 317);
            this.errorDisplay.Name = "errorDisplay";
            this.errorDisplay.Size = new System.Drawing.Size(99, 20);
            this.errorDisplay.TabIndex = 16;
            this.errorDisplay.Text = "Error Display";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 545);
            this.Controls.Add(this.errorDisplay);
            this.Controls.Add(this.directoryInput);
            this.Controls.Add(this.label0);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sizeInput);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox sizeInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label0;
        private System.Windows.Forms.TextBox directoryInput;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox heightMapInput;
        private System.Windows.Forms.TextBox plateMapInput;
        private System.Windows.Forms.TextBox timeStepInput;
        private System.Windows.Forms.TextBox velocityInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button movePlateButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox outNameInput;
        private System.Windows.Forms.TextBox genParamsInput;
        private System.Windows.Forms.TextBox cutoffInput;
        private System.Windows.Forms.TextBox numberOfPlatesInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button generationButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label errorDisplay;
    }
}

