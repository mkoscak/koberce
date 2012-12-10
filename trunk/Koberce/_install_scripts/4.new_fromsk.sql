-- uprava inventory tabulky, aby splnala normy
drop table FROMSK;

create table FROMSK (
	CODE TEXT UNIQUE,
	SELLDATE TEXT,
	SELLPRICE REAL,
	VALID TEXT DEFAULT "1"
);