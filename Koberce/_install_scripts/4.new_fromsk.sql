-- uprava inventory tabulky, aby splnala normy
drop table FROMSK;

create table FROMSK (
	CODE TEXT UNIQUE,
	VALID TEXT DEFAULT "1"
);