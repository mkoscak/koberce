-- pridanie platnosti do hlavnych tabuliek - nebude sa mazat ale zneplatnovat v pripade omylnych mazani sa budu dat data obnovit
ALTER TABLE arena
ADD Valid TEXT DEFAULT "1";

ALTER TABLE sold
ADD Valid TEXT DEFAULT "1";

ALTER TABLE inventory
ADD Valid TEXT DEFAULT "1";