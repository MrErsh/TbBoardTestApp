INSERT INTO Category(Id, [Name])
VALUES('00000000-0000-0000-0000-000000000001', N'Первая')

INSERT INTO Category(Id, [Name])
VALUES('00000000-0000-0000-0000-000000000002', N'Вторая')

INSERT INTO Category(Id, [Name])
VALUES('00000000-0000-0000-0000-000000000003', N'Третяя')


INSERT INTO Quote(Author, Content, CategoryId)
VALUES(N'Петров', N'Петрова цитата', '00000000-0000-0000-0000-000000000001')

INSERT INTO Quote(Author, Content, CategoryId)
VALUES(N'Иванов', N'Цитата иванова', '00000000-0000-0000-0000-000000000002')

INSERT INTO Quote(Author, Content, CategoryId)
VALUES(N'Безымянный', N'Отсутствие цитаты', '00000000-0000-0000-0000-000000000002')