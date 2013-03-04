-- uprava EXH1 tabulky, aby splnala normy
drop table EXH1;

create table EXH1 (
	Code TEXT UNIQUE,
	SellDate TEXT,
	SellPrice TEXT,
	ExhibitionName TEXT,
	Valid TEXT DEFAULT "1"
);