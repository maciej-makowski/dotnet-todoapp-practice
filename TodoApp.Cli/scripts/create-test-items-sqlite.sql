INSERT INTO tasks(title, itemType) VALUES ('Finish DB', 0);
INSERT INTO tasks(title, itemType) VALUES ('Finish list DB tasks', 1);
INSERT INTO tasks(title, itemType, parentId) 
	VALUES ('Magic subitem', 0, (SELECT taskId FROM tasks WHERE title = 'Finish list DB tasks'));