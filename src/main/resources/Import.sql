-- Active: 1686030576583@@127.0.0.1@3306@safad
INSERT INTO people(Document, Name, Adress, Telephone, Email, Password) VALUES ('100087678', 'Juan Eusse', 'Cra 95 # 23 - 43', '3018765676', 'jer2113@gmail.com', '1234');
INSERT INTO people(Document, Name, Adress, Telephone, Email, Password) VALUES ('1000653613', 'Pablo Zapata', 'Cra 40 # 103 - 22', '3136765688', 'jpz6@gmail.com', '1234');
INSERT INTO people(Document, Name, Adress, Telephone, Email, Password) VALUES ('1099996543', 'Pedro Pechuguita', 'Cra 100 # 101 - 11', '3136765699', 'pg1@gmail.com', '1234');
INSERT INTO people(Document, Name, Adress, Telephone, Email, Password) VALUES ('1077776543', 'Pepito Escamoso', 'Cra 21 # 210 - 9', '3116775699', 'pe2@gmail.com', '1234');
INSERT INTO people(Document, Name, Adress, Telephone, Email, Password) VALUES ('1055556543', 'Luisa Escamilla', 'Cra 14 # 122 - 8', '3006995699', 'le3@gmail.com', '1234');

INSERT INTO role(Name_Role) VALUES ('Entrenador');
INSERT INTO role(Name_Role) VALUES ('Deportista');
INSERT INTO role(Name_Role) VALUES ('Profesional');
INSERT INTO role(Name_Role) VALUES ('Familiar');
INSERT INTO role(Name_Role) VALUES ('Directivo');

INSERT INTO people_roles(people_id, rol_id) VALUES (1, 5);
INSERT INTO people_roles(people_id, rol_id) VALUES (2, 1);
INSERT INTO people_roles(people_id, rol_id) VALUES (3, 2);
INSERT INTO people_roles(people_id, rol_id) VALUES (4, 3);
INSERT INTO people_roles(people_id, rol_id) VALUES (5, 4);
