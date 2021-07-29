using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewVideoRentalProject
{
    public partial class VideoRentalForm : Form
    {
        public VideoRentalForm()
        {
            InitializeComponent();
            bookingDate.Value = DateTime.Now;
            dueDate.Value = DateTime.Now;
        }
        String dataType = "";
        String customerID = "";
        String videoID = "";
        String rentalID = "";
        String rentCost = "";

        private void outedRB_CheckedChanged(object sender, EventArgs e)
        {
            Database.GetOutRented(dataGV);
        }

        private void custBtn_Click(object sender, EventArgs e)
        {
            customerID = "";
            dataType = "Customer";
            Database.GetRenteeData(dataGV);
        }

        private void videoBtn_Click(object sender, EventArgs e)
        {
            videoID = "";
            dataType = "Video";
            Database.GetMovieDate(dataGV);
        }

        private void rentBtn_Click(object sender, EventArgs e)
        {
            rentalID = "";
            dataType = "Booking";
            Database.GetRentalData(dataGV);
        }

        private void rentedCB_CheckedChanged(object sender, EventArgs e)
        {
            Database.GetRented(dataGV);
        }

        private void VideoRentalForm_Load(object sender, EventArgs e)
        {
            Database.LoadPopular(popularVLbl, popularCLbl);
        }

        // grid view
        private void dataGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGV.Columns.Count != 0 && e.RowIndex != -1 && dataType != "" && e.ColumnIndex != -1)
            {
                DataGridViewRow row = dataGV.Rows[e.RowIndex];
                if (dataType == "Booking")
                {
                    rentalID = row.Cells["ID"].Value.ToString();
                    rentCost = row.Cells["Cost"].Value.ToString();
                    videoLbl.Text = row.Cells["VID"].Value.ToString();
                    custLbl.Text = row.Cells["CID"].Value.ToString();
                    bookingDate.Text = row.Cells["Booking Date"].Value.ToString();
                    dueDate.Text = row.Cells["Return Date"].Value.ToString();
                }
                else if (dataType == "Customer")
                {
                    custLbl.Text = row.Cells["ID"].Value.ToString();
                    customerID = row.Cells["ID"].Value.ToString();
                    nameTxt.Text = row.Cells["Name"].Value.ToString();
                    addTxt.Text = row.Cells["Address"].Value.ToString();
                    conctactTxt.Text = row.Cells["Phone"].Value.ToString();
                }
                else if (dataType == "Video")
                {
                    videoID = row.Cells["ID"].Value.ToString();
                    videoLbl.Text = row.Cells["ID"].Value.ToString();
                    videoTitleTxt.Text = row.Cells["Title"].Value.ToString();
                    videoCostTxt.Text = row.Cells["Cost"].Value.ToString().Remove(1, 2);
                    videoYearTxt.Value = new DateTime(Convert.ToInt32(row.Cells["Year"].Value.ToString()), 1, 1);
                    videoCopiesTxt.Text = row.Cells["Copies"].Value.ToString();
                    videoGenerTxt.Text = row.Cells["Gener"].Value.ToString();
                    videoRattingTxt.Text = row.Cells["Ratting"].Value.ToString();
                }
            }
        }
        private void custAddBtn_Click(object sender, EventArgs e)
        {
            customerID = "";
            if (nameTxt.Text != "" && addTxt.Text != "" && conctactTxt.Text != "")
            {
                Database.AddData(nameTxt, conctactTxt, addTxt);
                custBtn.PerformClick();
            }
        }

        private void custUpdateBtn_Click(object sender, EventArgs e)
        {
            if (customerID != "" && nameTxt.Text != "")
            {
                Database.UpdateData(nameTxt, conctactTxt, addTxt, customerID);
                customerID = "";
                custBtn.PerformClick();
            }
        }

        // customer delete Button

        private void custDeleteBtn_Click(object sender, EventArgs e)
        { 
            if (customerID != "" && nameTxt.Text != "")
            {
                Database.DeleteData(nameTxt, conctactTxt, addTxt, customerID);
                custBtn.PerformClick();
                customerID = "";
            }
        }
        // add video Button

        private void videoAddBtn_Click(object sender, EventArgs e)
        {
            videoID = "";
            if (videoTitleTxt.Text != "" && videoCostTxt.Text != "" && videoCopiesTxt.Text != "" && videoYearTxt.Text != "")
            {
                Database.AddData(videoTitleTxt, videoGenerTxt, videoCostTxt, videoRattingTxt, videoCopiesTxt, videoYearTxt);
                videoBtn.PerformClick();
            }
        }

        // video delete

        private void videoDeleteBtn_Click(object sender, EventArgs e)
        {
            if (videoID != "" && videoTitleTxt.Text != "")
            {
                if (videoTitleTxt.Text != "" && videoCostTxt.Text != "" && videoCopiesTxt.Text != "" && videoYearTxt.Text != "")
                {
                    Database.DeleteData(videoTitleTxt, videoGenerTxt, videoCostTxt, videoRattingTxt, videoCopiesTxt, videoID);
                    videoID = "";
                    videoBtn.PerformClick();
                }
            }
        }
        // video update button 

        private void videoUpdateBtn_Click(object sender, EventArgs e)
        {
            if (videoID != "")
            {
                if (videoTitleTxt.Text != "" && videoCostTxt.Text != "" && videoCopiesTxt.Text != "" && videoYearTxt.Text != "")
                {
                    Database.UpdateData(videoTitleTxt, videoGenerTxt, videoCostTxt, videoRattingTxt, videoCopiesTxt, videoYearTxt, videoID);
                    videoBtn.PerformClick();
                    videoID = "";
                }
            }
        }

        // remove button
        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (rentalID != "" && videoLbl.Text != "" && custLbl.Text != "")
            {
                Database.DeleteData(rentalID);
                custLbl.Text = "";
                videoLbl.Text = "";
                videoTitleTxt.Text = "";
                videoGenerTxt.Text = "";
                videoCopiesTxt.Text = "";
                videoRattingTxt.Text = "";
                videoCostTxt.Text = "";
                nameTxt.Text = "";
                addTxt.Text = "";
                conctactTxt.Text = "";
                rentalID = "";
                rentBtn.PerformClick();
            }
        }

        // issue new Movie
        private void issueBtn_Click(object sender, EventArgs e)
        {
            rentalID = "";
            if (videoLbl.Text != "" && custLbl.Text != "")
            {
                Database.AddData(custLbl, videoLbl, bookingDate, dueDate);
                rentBtn.PerformClick();
                videoLbl.Text = "";
                custLbl.Text = "";
                videoTitleTxt.Text = "";
                videoGenerTxt.Text = "";
                videoCopiesTxt.Text = "";
                videoRattingTxt.Text = "";
                videoCostTxt.Text = "";
                nameTxt.Text = "";
                addTxt.Text = "";
                conctactTxt.Text = "";
            }
        }
        //return Viedo Button

        private void returnBtn_Click(object sender, EventArgs e)
        {
            if (videoLbl.Text != "" && custLbl.Text != "" && rentalID != "")
            {
                int a = Convert.ToInt32(rentCost) * Convert.ToInt32((dueDate.Value - bookingDate.Value).TotalDays);
                if (a == 0)
                    a = Convert.ToInt32(rentCost);
                Database.UpdateData(custLbl, videoLbl, bookingDate, dueDate, rentalID, a);
                videoLbl.Text = "";
                custLbl.Text = "";
                rentalID = "";
                videoTitleTxt.Text = "";
                videoGenerTxt.Text = "";
                videoCopiesTxt.Text = "";
                videoRattingTxt.Text = "";
                videoCostTxt.Text = "";
                nameTxt.Text = "";
                addTxt.Text = "";
                conctactTxt.Text = "";
                rentBtn.PerformClick();
            }
        }

        private void VideoRentalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        // Video cost as per year
        private void videoYearTxt_ValueChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year - videoYearTxt.Value.Year);
            if (a >= 5)
                videoCostTxt.Text = "2";
            else
                videoCostTxt.Text = "5";
        }

        private void videoTitleTxt_TextChanged(object sender, EventArgs e)
        {
            int a = Convert.ToInt32(DateTime.Now.Year - videoYearTxt.Value.Year);
            if (a >= 5)
                videoCostTxt.Text = "2";
            else
                videoCostTxt.Text = "5";
        }

        private void conctactTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void videoCopiesTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}