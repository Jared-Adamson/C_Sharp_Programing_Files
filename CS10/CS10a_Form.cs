﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; 

namespace WindowsFormsApplication1
{
   

    public partial class Form1 : Form
    {
         string bookName = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bookName = bookBox.Text;

            //Add a new book

            if (bookBox.Text != "")
            {
                bookBox.Items.Add(bookName);
            }
            else
            {
              
                MessageBox.Show("Please enter a book title to add.", "Missing data.",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            bookBox.Focus();

        }

        private void removeBook_Click(object sender, EventArgs e)
        {
            //Remove a book

            if (bookBox.SelectedIndex != -1)
            {
                bookBox.Items.RemoveAt(bookBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Please select a book to remove.",
                    "No selection made", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void clearBookListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Clear the list
            DialogResult responseDialogResult;

            responseDialogResult = MessageBox.Show("Clear the entire book list?",
                "Clear book list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (responseDialogResult == DialogResult.Yes)
            {
                bookBox.Items.Clear();
                bookBox.Text = "";
            }
        }

        private void displayBookCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Display a book count
            MessageBox.Show("The number of books in the list is -  " + bookBox.Items.Count.ToString());
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create an instance and display frmAbout 
            aboutForm frmAboutObj = new aboutForm();
            frmAboutObj.ShowDialog();   //.ShowDialog displays as a Modal Form
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Print Preview
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Print the thing
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Printing handler

            Font printFont = new Font("Arial", 12);
            Font headingFont = new Font("Arial", 14, FontStyle.Bold);
            float fltLineHeight = printFont.GetHeight();
            float fltPrintX = e.MarginBounds.Left;
            float fltPrintY = e.MarginBounds.Top;
            string strPrintLine;


            strPrintLine = "Book List ";
            e.Graphics.DrawString(strPrintLine, headingFont,
                Brushes.Black, fltPrintX, fltPrintY);
            strPrintLine = "by Jared Adamson";
            fltPrintY += fltLineHeight;
            e.Graphics.DrawString(strPrintLine, headingFont,
                Brushes.Black, fltPrintX, fltPrintY);

            
            fltPrintY += fltLineHeight * 2;

            
            for (int intIndex = 0; intIndex <= bookBox.Items.Count - 1; intIndex++)
            {
              
                strPrintLine = bookBox.Items[intIndex].ToString();
               
                e.Graphics.DrawString(strPrintLine, printFont,
                    Brushes.Black, fltPrintX, fltPrintY);

              
                fltPrintY += fltLineHeight;
        }

    
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loadBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Not checking if a list has already been loaded
            string strFileName;
            string bookName;

            //Open the file and load the list box with the data stored in the file
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1; // At first only show text files.
                openFileDialog1.InitialDirectory = Application.StartupPath; //default folder
                openFileDialog1.RestoreDirectory = true;  // restore path to default folder

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strFileName = openFileDialog1.FileName;
                    FileStream booksFileIn = new FileStream(strFileName, FileMode.Open);
                    StreamReader flavorsStreamReader = new StreamReader(booksFileIn);
                    while (flavorsStreamReader.Peek() != -1)
                    {
                        bookName = flavorsStreamReader.ReadLine();
                        bookBox.Items.Add(bookName);
                    }
                    flavorsStreamReader.Close();
                }
                else
                {
                    MessageBox.Show("File not selected; List was not loaded.",
                            "List Not Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Selected file could not be opened.",
                        "Error Opening File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }	
    }
}
