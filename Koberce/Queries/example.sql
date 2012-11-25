-- Tu pride nazov, ktory sa zobrazi ako text tlacitka (musi zacinat //)
select *
from sold s
join arena a on s.code = a.code
where code = 101102
