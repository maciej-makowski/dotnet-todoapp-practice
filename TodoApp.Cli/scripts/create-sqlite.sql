﻿CREATE TABLE tasks (
	taskId INTEGER PRIMARY KEY AUTOINCREMENT,
	insertedAt DATETIME NOT NULL DEFAULT(datetime('now')),
	title TEXT NOT NULL,
	completed BOOLEAN NOT NULL DEFAULT(false),
	itemType INTEGER,
	parentId INTEGER NULL DEFAULT(NULL) REFERENCES tasks(taskId)
);