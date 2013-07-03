BEGIN TRANSACTION;

ALTER TABLE arena RENAME TO arena_old;

CREATE TABLE arena (
    "Code" TEXT,
    "ItemName" TEXT,
    "Country" TEXT,
    "Supplier" TEXT,
    "Supplier_Nr" TEXT,
    "Length" INTEGER,
    "Width" INTEGER,
    "EK_Netto" TEXT,
    "VK_Netto" TEXT,
    "Quantity" TEXT,
    "QM_price" TEXT,
    "Date" TEXT,
    "mvDate" TEXT,
    "Invoice" TEXT, 
	"Color" TEXT, 
	"Material" TEXT, 
	"Comment" TEXT, 
	"Info" TEXT, 
	"Euro_Stuck" TEXT, 
	"Valid" TEXT DEFAULT "1", 
	"Paid" TEXT
);

-- insert
INSERT INTO arena(Code, Supplier_Nr, ItemName, Country, Supplier, Length, Width, EK_Netto, Quantity, VK_Netto, Date, Paid, mvDate, Invoice, Color, Material, Comment, Valid, Info, Euro_Stuck, QM_price)
SELECT Code, Supplier_Nr, ItemTitle, Country, Supplier, Length, Width, EK_Netto, Quantity, VK_Netto, Date, Paid, mvDate, Invoice, Color, Material, Comment, Valid, RgNr, Euro_Stuck, EK_Netto
FROM arena_old;

-- stare stlpce pre istotu nechame..
--DROP TABLE arena_old;

-- najprv vybrat vsetko okrem tohto textu a ak vsetko ok, potom vybrat a spustit commit;
COMMIT TRANSACTION;
