using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test0912_CRport {
    //兩欄式報表
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            da.Fill(ds);
            ProductReport p = new ProductReport(); //創造出報表
            //p.SetDataSource(ds);
            p.SetDataSource(ds.Tables["Products"]);
            crystalReportViewer1.ReportSource = p;
        }

        private void button2_Click(object sender, EventArgs e) {
            da.Fill(ds);
            GroudProductReport p = new GroudProductReport();
            p.SetDataSource(ds.Tables["Products"]);
            crystalReportViewer1.ReportSource = p;
        }

        private void button3_Click(object sender, EventArgs e) {

            da.Fill(ds);

            var queryALL = from p in ds.Products
                           select new { id = p.ProductID, Name = p.ProductName };

            dataGridView1.DataSource = queryALL.ToList();
            foreach (var q in queryALL) {
                NorthWindDataSet.TwoDataTableRow dr = ds.TwoDataTable.NewTwoDataTableRow();
                if (q.id % 2 != 0) {
                    dr.idLeft = q.id.ToString();
                    dr.nameLeft = q.Name;
                } else {
                    dr.idRight = q.id.ToString();
                    dr.nameRight = q.Name;
                }
                ds.TwoDataTable.Rows.Add(dr);
            }

            TwoCrystalReport two = new TwoCrystalReport();
            two.SetDataSource(ds.Tables["TwoDataTable"]);
            crystalReportViewer1.ReportSource = two;
        }

        private void button4_Click(object sender, EventArgs e) {
            da.Fill(ds);
            NorthWindDataSet.TwoDataTableRow drTwoColumns = null;
            int i = 0;
            foreach (NorthWindDataSet.ProductsRow drProduct in ds.Products) {
                i++;
                if (i % 2 == 1) {
                    drTwoColumns = ds.TwoDataTable.NewTwoDataTableRow();
                    drTwoColumns.idLeft = drProduct.ProductID.ToString();
                    drTwoColumns.nameLeft = drProduct.ProductName;
                }else {
                    
                    drTwoColumns.idRight = drProduct.ProductID.ToString();
                    drTwoColumns.nameRight = drProduct.ProductName;
                    ds.TwoDataTable.AddTwoDataTableRow(drTwoColumns);
                }
            }
            if (i % 2 == 1) {
                drTwoColumns.idRight = "N/A";
                drTwoColumns.nameRight = "N/A";
                ds.TwoDataTable.AddTwoDataTableRow(drTwoColumns);
            }

            TwoCrystalReport r = new TwoCrystalReport();
            r.SetDataSource(ds.Tables["TwoDataTable"]);
            crystalReportViewer1.ReportSource = r;
        }
    }
}
