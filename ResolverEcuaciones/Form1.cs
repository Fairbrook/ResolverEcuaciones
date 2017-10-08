using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpMatrices;
using OpMatrices.Proceso;
using OpMatrices.Types;

namespace ResolverEcuaciones
{
    public partial class Form1 : Form
    {
        int num;
        TextBox[,] myText;
        TextBox[] txtResult;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                btnNuevo.Visible = true;
                num = Convert.ToInt32(txtNum.Text);
                myText = new TextBox[num, num + 1];
                txtResult = new TextBox[num];
                Label mylabel;
                for (int i = 0; i < num; i++)
                {
                    mylabel = new Label();
                    mylabel.Location = new Point(5, 80 + (i * 27));
                    mylabel.Text = "E" + (i + 1);
                    mylabel.Size = new System.Drawing.Size(20, 15);
                    this.Controls.Add(mylabel);

                    for (int j = 0; j < num; j++)
                    {
                        if (i == 0)
                        {
                            mylabel = new Label();
                            mylabel.Location = new Point(38 + (j * 60), 65 + (i * 25));
                            mylabel.Size = new System.Drawing.Size(20, 15);
                            mylabel.Text = "X" + (j + 1);
                            this.Controls.Add(mylabel);
                        }
                        mylabel = new Label();
                        myText[i, j] = new TextBox();

                        myText[i, j].Location = new Point(25 + (j * 60), 80 + (i * 25));
                        myText[i, j].Name = "txtDat" + i;
                        myText[i, j].Size = new System.Drawing.Size(50, 20);

                        mylabel.Location = new Point(75 + (j * 60), 80 + (i * 25));
                        if (j == num - 1) mylabel.Text = "=";
                        else mylabel.Text = "+";
                        mylabel.Size = new System.Drawing.Size(10, 10);

                        this.Controls.Add(mylabel);
                        this.Controls.Add(myText[i, j]);
                    }
                }
                mylabel = new Label();
                mylabel.Location = new Point(38 + (num * 60), 65);
                mylabel.Size = new System.Drawing.Size(20, 15);
                mylabel.Text = "R";
                this.Controls.Add(mylabel);

                for (int i = 0; i < num; i++)
                {
                    mylabel = new Label();
                    txtResult[i] = new TextBox();
                    txtResult[i].Location = new Point(25 + (num * 60), 80 + (i * 25));
                    txtResult[i].Name = "txtDat" + i;
                    txtResult[i].Size = new System.Drawing.Size(50, 20);

                    mylabel.Location = new Point(75 + (num * 60), 80 + (i * 25));
                    mylabel.Text = " ";
                    mylabel.Size = new System.Drawing.Size(10, 10);

                    this.Controls.Add(mylabel);
                    this.Controls.Add(txtResult[i]);

                }
                txtNum.Enabled = false;
                btnResult.Visible = true;
                btnOk.Visible = false;
                btnResult.Location = new Point(25, 80 + (num * 25));
            }
            catch (Exception) { MessageBox.Show("Cantidad inválida"); }
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            Label lblRs;
            Fraccion[,] frac = new Fraccion[num, num];
            double[,] array = new double[num,1];
            Matriz<Fraccion> MatrizA = new Matriz<Fraccion>();
            Matriz<double> MatrizB = new Matriz<double>();
            Matriz<double> MatrizAP = new Matriz<double>();
            try
            {
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        frac[i, j] = new Fraccion();
                        frac[i, j].setFrac(Convert.ToInt32(myText[i, j].Text), 1);
                    }
                }
                MatrizA.setMatriz(num, num, frac);
                for (int i = 0; i < num; i++)
                {
                    array[i, 0] = Convert.ToDouble(txtResult[i].Text);
                }
                MatrizB.setMatriz(num, 1, array);
                MatrizA = Operaciones.inver(MatrizA);
                MatrizAP = Operaciones.MtrzFracToDouble(MatrizA);
                MatrizB = Operaciones.mult(MatrizAP, MatrizB);
                array = MatrizB.getMatriz();
                for (int i = 0; i < num; i++)
                {
                    lblRs = new Label();
                    lblRs.Location = new Point(25, 80 + ((num+1+i) * 25));
                    lblRs.Text = "X" + (i + 1) + ": " + array[i,0];
                    lblRs.Size = new System.Drawing.Size(50, 20);
                    this.Controls.Add(lblRs);
                }

                btnResult.Location = new Point(25, 80 + (num * 25));
            }
            catch(Exception)
            {
                MessageBox.Show("Cantidad Inválida");
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Form1 formulario = new Form1();
            formulario.Show();
            this.Dispose(false);
        }
    }
}
