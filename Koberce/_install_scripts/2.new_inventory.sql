-- uprava inventory tabulky, aby splnala normy
drop table inventory;

create table inventory (
	CODE TEXT UNIQUE
);