create table if not exists author
(
	authorid serial not null
		constraint author_pk
			primary key,
	name varchar(150) not null,
	about varchar(500)
);

comment on table author is 'Authors Writters o books';

alter table author owner to appuser;

create table if not exists book
(
	bookid serial not null
		constraint book_pk
			primary key,
	authorid integer
		constraint book_author_authorid_fk
			references author,
	name varchar(150),
	about varchar(500),
	place varchar(150)
);

comment on table book is 'Books table';

alter table book owner to appuser;

create table useracess
(
	userid serial not null
		constraint useraccess_pk
			primary key,
	name varchar(250),
	email varchar(250) not null,
	password varchar(350) not null
);

comment on table useraccess is 'Users login';

alter table useraccess owner to appuser;