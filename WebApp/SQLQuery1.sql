/*==============================================================*/
/* Table: Autors                                                */
/*==============================================================*/
create table Autors (
   id					int               IDENTITY not null,
   Name                 nvarchar(50)                 null,
   LastName             nvarchar(50)                 null,
   constraint PK_AUTORS primary key nonclustered (id)
)
go

/*==============================================================*/
/* Table: Books                                                 */
/*==============================================================*/
create table Books (
   id               int                  IDENTITY not null,
   Name                 nvarchar(50)                 null,
   constraint PK_BOOKS primary key nonclustered (id)
)
go

/*==============================================================*/
/* Table: BooksAutors                                           */
/*==============================================================*/
create table AutorBook (
   autorID              int               null,
   bookID               int                  null
)
go


/*==============================================================*/
/* Table: Users                                                 */
/*==============================================================*/
create table Users (
   id		            int               IDENTITY not null,
   Name                 nvarchar(50)                 null,
   LastName             nvarchar(50)                 null,
   constraint PK_USERS primary key nonclustered (id)
)
go

/*==============================================================*/
/* Table: UsersBooks                                            */
/*==============================================================*/
create table UserBook (
   bookID               int                  null,
   userID				int					 null,
)
go


alter table AutorBook
   add constraint FK_BOOKSAUT_RELATIONS_BOOKS foreign key (bookID)
      references Books (id)
go

alter table AutorBook
   add constraint FK_BOOKSAUT_RELATIONS_AUTORS foreign key (autorID)
      references Autors (id)
go

alter table UserBook
   add constraint FK_USERSBOO_RELATIONS_BOOKS foreign key (bookID)
      references Books (id)
go

alter table UserBook
   add constraint FK_USERSBOO_RELATIONS_USERS foreign key (userID)
      references Users (id)
go