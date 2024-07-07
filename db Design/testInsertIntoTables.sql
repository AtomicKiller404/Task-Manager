INSERT INTO Users (username, password, email) VALUES ('john_doe', 'password123', 'john.doe@example.com');
INSERT INTO Users (username, password, email) VALUES ('jane_smith', 'password456', 'jane.smith@example.com');
INSERT INTO Users (username, password, email) VALUES ('alice_jones', 'password789', 'alice.jones@example.com');

INSERT INTO Projects (title, description) VALUES ('Project Alpha', 'Description for Project Alpha');
INSERT INTO Projects (title, description) VALUES ('Project Beta', 'Description for Project Beta');
INSERT INTO Projects (title, description) VALUES ('Project Gamma', 'Description for Project Gamma');

INSERT INTO Task_Status (name) VALUES ('To Do');
INSERT INTO Task_Status (name) VALUES ('In Progress');
INSERT INTO Task_Status (name) VALUES ('Completed');

INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 1', 'Description for Task 1', 1, 1, '2024-07-15');
INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 2', 'Description for Task 2', 2, 1, '2024-07-20');
INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 3', 'Description for Task 3', 3, 2, '2024-08-01');
INSERT INTO Tasks (title, description, status_id, project_id, due_date) VALUES ('Task 4', 'Description for Task 4', 1, 3, '2024-08-15');

INSERT INTO UserTasks (user_id, task_id) VALUES (1, 1);
INSERT INTO UserTasks (user_id, task_id) VALUES (2, 2);
INSERT INTO UserTasks (user_id, task_id) VALUES (1, 3);
INSERT INTO UserTasks (user_id, task_id) VALUES (3, 4);
