-- uprava SK tabulky, aby splnala normy
drop table SK;

create table SK (
	CODE TEXT UNIQUE,
	VALID TEXT DEFAULT "1"
);