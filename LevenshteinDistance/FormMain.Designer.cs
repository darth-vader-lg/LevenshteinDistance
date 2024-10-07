namespace TestRegExp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxText = new TextBox();
            labelOutput = new Label();
            SuspendLayout();
            // 
            // textBoxText
            // 
            textBoxText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxText.Location = new Point(12, 12);
            textBoxText.Multiline = true;
            textBoxText.Name = "textBoxText";
            textBoxText.Size = new Size(776, 367);
            textBoxText.TabIndex = 0;
            textBoxText.TextChanged += TextBoxText_TextChanged;
            // 
            // labelOutput
            // 
            labelOutput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelOutput.Location = new Point(12, 382);
            labelOutput.Name = "labelOutput";
            labelOutput.Size = new Size(776, 59);
            labelOutput.TabIndex = 1;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelOutput);
            Controls.Add(textBoxText);
            Name = "FormMain";
            Text = "Levenshtein distance";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxText;
        private Label labelOutput;
    }
}
