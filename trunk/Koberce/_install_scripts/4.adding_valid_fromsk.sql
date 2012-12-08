-- pridanie platnosti do hlavnych tabuliek - nebude sa mazat ale zneplatnovat v pripade omylnych mazani sa budu dat data obnovit
ALTER TABLE fromSK
ADD Valid TEXT DEFAULT "1";
