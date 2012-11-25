-- uprava sold tabulky, aby splnala normy
drop table SOLD;

create table SOLD (
	Code TEXT UNIQUE,
	SellDate TEXT,
	SellPrice TEXT
);