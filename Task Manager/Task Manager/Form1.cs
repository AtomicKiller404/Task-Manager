using Microsoft.Data.Sqlite;
using System;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (TestDatabaseConnection())
                CreateDatabase();
             
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private bool TestDatabaseConnection()
        {
            string connectionString = "Data Source=taskmanager.db";
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened successfully.");
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Database Error: " + e.Message);
                Console.WriteLine("Inner Exception: " + e.InnerException?.Message);
            }

            return false;
        }

        private void CreateDatabase()
        {
            try
            {
                using (var connection = new SqliteConnection("Data Source=taskmanager.db"))
                {
                    connection.Open();

                    // Create Users table
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Users (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            username TEXT NOT NULL UNIQUE,
                            password TEXT NOT NULL,
                            email TEXT NOT NULL UNIQUE,
                            created_at DATETIME DEFAULT CURRENT_TIMESTAMP
                        );");

                    // Create Projects table
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Projects (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            title TEXT NOT NULL,
                            description TEXT,
                            created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                            updated_at DATETIME DEFAULT CURRENT_TIMESTAMP
                        );");

                    // Create Task_Status table
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Task_Status (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name TEXT NOT NULL UNIQUE
                        );");

                    // Create Tasks table
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS Tasks (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            title TEXT NOT NULL,
                            description TEXT,
                            status_id INTEGER,
                            project_id INTEGER,
                            due_date DATE,
                            created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                            updated_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (status_id) REFERENCES Task_Status(id),
                            FOREIGN KEY (project_id) REFERENCES Projects(id)
                        );");

                    // Create UserTasks table
                    ExecuteNonQuery(connection, @"
                        CREATE TABLE IF NOT EXISTS UserTasks (
                            user_id INTEGER,
                            task_id INTEGER,
                            PRIMARY KEY (user_id, task_id),
                            FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
                            FOREIGN KEY (task_id) REFERENCES Tasks(id) ON DELETE CASCADE
                        );");

                    connection.Close();
                    Console.WriteLine("Database and tables created successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Database Error: " + e.Message);
            }
        }

        private void ExecuteNonQuery(SqliteConnection connection, string commandText)
        {
            using (var command = new SqliteCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
