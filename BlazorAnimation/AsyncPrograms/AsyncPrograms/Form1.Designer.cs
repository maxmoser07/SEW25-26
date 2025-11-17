namespace AsyncPrograms;

partial class Form1
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
        button1 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        label2 = new System.Windows.Forms.Label();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Location = new System.Drawing.Point(330, 98);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(176, 27);
        button1.TabIndex = 0;
        button1.Text = "Sieb ohne Thread";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(105, 246);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(189, 58);
        label1.TabIndex = 4;
        label1.Text = "label1";
        // 
        // textBox1
        // 
        textBox1.Location = new System.Drawing.Point(105, 167);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(195, 27);
        textBox1.TabIndex = 5;
        textBox1.Tag = "L";
        textBox1.TextChanged += textBox1_TextChanged;
        // 
        // textBox2
        // 
        textBox2.Location = new System.Drawing.Point(105, 98);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(195, 27);
        textBox2.TabIndex = 6;
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(512, 102);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(100, 23);
        label2.TabIndex = 7;
        label2.Text = "label2";
        // 
        // button2
        // 
        button2.Location = new System.Drawing.Point(330, 167);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(176, 27);
        button2.TabIndex = 8;
        button2.Text = "Sieb mit Thread";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // button3
        // 
        button3.Location = new System.Drawing.Point(330, 243);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(176, 27);
        button3.TabIndex = 9;
        button3.Text = "Backgroundworker";
        button3.UseVisualStyleBackColor = true;
        button3.Click += button3_Click;
        // 
        // button4
        // 
        button4.Location = new System.Drawing.Point(330, 313);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(176, 27);
        button4.TabIndex = 10;
        button4.Text = "Async Sieb";
        button4.UseVisualStyleBackColor = true;
        button4.Click += button4_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(button4);
        Controls.Add(button3);
        Controls.Add(button2);
        Controls.Add(label2);
        Controls.Add(textBox2);
        Controls.Add(textBox1);
        Controls.Add(label1);
        Controls.Add(button1);
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;

    #endregion
}