--delete from categoria 
select id, nome, idauxiliar, IdCategoriaPai
from categoria 
--where id=487 or IdCategoriaPai=487
--where id in (1026,1028,1030,1020,1022,1024,1014,1016,1018,1012,1032)
order by IdCategoriaPai, id