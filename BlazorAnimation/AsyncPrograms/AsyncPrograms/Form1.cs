using System.ComponentModel;

namespace AsyncPrograms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }


    private void button1_Click(object sender, EventArgs e) //button 1
    {
        label2.Text = "..loading..";
        int upto = int.Parse(textBox2.Text);
        int result;
        Sieve(upto, out result);
        label2.Text = result.ToString();
    }

    private void button2_Click(object sender, EventArgs e) //button 2
    {
        label2.Text = "..loading..";
        int upto = int.Parse(textBox2.Text);
        int result = 0;
        Thread t = new Thread((x)=> Sieve(upto, out result));
        t.Start();
        t.Join();
        label2.Text = result.ToString();
    }

    private void button3_Click(object sender, EventArgs e) //button 3
    {
        label2.Text = "..loading..";
        int upto = int.Parse(textBox2.Text);
        int result;
        
        BackgroundWorker worker = new BackgroundWorker();
        worker.DoWork += (s, e) =>
        {
            Sieve(upto, out result);
            e.Result = result;
        };
        worker.RunWorkerCompleted += (s, e) =>
        {
            label2.Text = e.Result.ToString();
        };
    }

    private async void button4_Click(object sender, EventArgs e) //button 4
    {
        label2.Text = "..loading..";
        int upto = int.Parse(textBox2.Text);
        int result = 0;
        await Task.Run(() => Sieve(upto, out result));
        label2.Text = result.ToString();
    }
    

    private void textBox1_TextChanged(object sender, EventArgs e) //textbox 1
    {
        label1.Text = textBox1.Text;
    }
    
    //methodes
    public void Sieve(int n, out int result) //methode 1
    {
        bool[] isPrime = new bool[n+1];
        for (int i = 2; i <= n; i++)
        {
            isPrime[i] = true;
        }
        for (int i = 2; i * i <= n; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= n; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }
        result = isPrime.Count(x => x == true);
    }
    
}