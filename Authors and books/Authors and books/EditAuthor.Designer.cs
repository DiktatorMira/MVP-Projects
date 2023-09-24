namespace Authors_and_books
{
    partial class EditAuthor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAuthor));
            label1 = new Label();
            textBox1 = new TextBox();
            button2 = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 9);
            label1.Name = "label1";
            label1.Size = new Size(199, 20);
            label1.TabIndex = 2;
            label1.Text = "Введите новое имя автора:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(7, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(353, 27);
            textBox1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(7, 65);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 6;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(266, 65);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 7;
            button1.Text = "Ок";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // EditAuthor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 99);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "EditAuthor";
            Text = "Редактирование автора";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button button2;
        private Button button1;
    }
}