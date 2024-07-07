using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_Manager
{
    public partial class Form1 : Form
    {
        const string connectionString = @"Data Source=db\taskmanager.db";

        public Form1()
        {
            InitializeComponent();
            if (TestDatabaseConnection())
            {
                CreateDatabase();
                ReadData();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private bool TestDatabaseConnection()
        {
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
                using (var connection = new SqliteConnection(connectionString))
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

        private void insert()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    ExecuteNonQuery(connection, @"INSERT INTO Users (username, password, email) VALUES ('john_doe', 'password123', 'john.doe@example.com');");
                    ExecuteNonQuery(connection, @"INSERT INTO Users (username, password, email) VALUES ('jane_smith', 'password456', 'jane.smith@example.com');");
                    ExecuteNonQuery(connection, @"INSERT INTO Users (username, password, email) VALUES ('alice_jones', 'password789', 'alice.jones@example.com');");
                    ExecuteNonQuery(connection, @"INSERT INTO Projects (title, description) VALUES ('Project Alpha', 'Description for Project Alpha');");
                    ExecuteNonQuery(connection, @"INSERT INTO Projects (title, description) VALUES ('Project Beta', 'Description for Project Beta');");
                    ExecuteNonQuery(connection, @"INSERT INTO Projects (title, description) VALUES ('Project Gamma', 'Description for Project Gamma');");
                    ExecuteNonQuery(connection, @"INSERT INTO Task_Status (name) VALUES ('To Do');");
                    ExecuteNonQuery(connection, @"INSERT INTO Task_Status (name) VALUES ('In Progress');");
                    ExecuteNonQuery(connection, @"INSERT INTO Task_Status (name) VALUES ('Completed');");
                    ExecuteNonQuery(connection, @"INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 1', 'Description for Task 1', 1, 1, '2024-07-15');");
                    ExecuteNonQuery(connection, @"INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 2', 'Description for Task 2', 2, 1, '2024-07-20');");
                    ExecuteNonQuery(connection, @"INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 3', 'Description for Task 3', 3, 2, '2024-08-01');");
                    ExecuteNonQuery(connection, @"INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 4', 'Description for Task 4', 1, 3, '2024-08-15');");
                    ExecuteNonQuery(connection, @"INSERT INTO UserTasks (user_id, task_id) VALUES (1, 1);");
                    ExecuteNonQuery(connection, @"INSERT INTO UserTasks (user_id, task_id) VALUES (2, 2);");
                    ExecuteNonQuery(connection, @"INSERT INTO UserTasks (user_id, task_id) VALUES (1, 3);");
                    ExecuteNonQuery(connection, @"INSERT INTO UserTasks (user_id, task_id) VALUES (3, 4);");

                }
                catch (Exception e)
                {
                    Console.WriteLine("Database Error: " + e.Message);
                    MessageBox.Show("Database Error: " + e.Message);
                }
                finally
                {
                    connection.Close(); // Always close the connection after using it
                }
            }
        }

        private void ReadData()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Users";
                    using (var command = new SqliteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string username = reader["username"].ToString(); // Get username from reader
                                Console.WriteLine("Username: " + username);
                                // You can append usernames to a list or display them in UI elements as needed
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database Error: " + e.Message);
                    MessageBox.Show("Database Error: " + e.Message);
                }
                finally
                {
                    connection.Close(); // Always close the connection after using it
                }
            }
        }

    }
}
