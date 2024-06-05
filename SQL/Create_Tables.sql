CREATE TABLE IF NOT EXISTS "Make"(
	"Id" UUID DEFAULT gen_random_uuid(),
	"Name" VARCHAR(100) NOT NULL,
	CONSTRAINT "PK_Make" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Vehicle"(
	"Id" UUID DEFAULT gen_random_uuid(),
	"MakeId" UUID,
	"Model" VARCHAR(100),
	"Colour" VARCHAR(40) NOT NULL,
	"Year" TIMESTAMPTZ,
	"ForSale" BOOLEAN NOT NULL,
	CONSTRAINT "PK_Vehicle" PRIMARY KEY ("Id"),
	CONSTRAINT "FK_Vehicle_Make_MakeId" FOREIGN KEY ("MakeId") REFERENCES "Make"("Id")
);

ALTER TABLE "Vehicle"
	RENAME COLUMN "Colour" TO "Color";