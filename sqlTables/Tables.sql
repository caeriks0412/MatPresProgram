CREATE TABLE InputMaterials(
    name TEXT PRIMARY KEY
);

CREATE TABLE MassFlowStockpiles(
    name TEXT PRIMARY KEY,
    flow NUMERIC,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE ProductionHours(
    name TEXT PRIMARY KEY,
    hours INT,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE BulkDensity(
    name TEXT PRIMARY KEY,
    density NUMERIC,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE MoistureContent(
    name TEXT PRIMARY KEY,
    moisture NUMERIC,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE WorkIndex(
    name TEXT PRIMARY KEY,
    index NUMERIC,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE TotalYearlyProduction(
    name TEXT PRIMARY KEY,
    totalProduction INT,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE TotalBlastedMaterial(
    name TEXT PRIMARY KEY,
    totalBlasted INT,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE TotalExternalMaterial(
    name TEXT PRIMARY KEY,
    totalExternal INT,
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE DieselConsumption(
    name TEXT PRIMARY KEY,
    dieselCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('l', 'm3', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE ElectricityConsumption(
    name TEXT PRIMARY KEY,
    electricityCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('kwh')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE WaterConsumption(
    name TEXT PRIMARY KEY,
    waterCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE ExplosivesConsumption(
    name TEXT PRIMARY KEY,
    explosivesCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE OilConsumption(
    name TEXT PRIMARY KEY,
    oilConsumption NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE MetalsConsumption(
    name TEXT PRIMARY KEY,
    metalCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE RubberConsumption(
    name TEXT PRIMARY KEY,
    rubberCon NUMERIC,
    unit TEXT,
    CHECK (unit IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE ChemicalsConsumption(
    name TEXT PRIMARY KEY,
    chemicalsCons NUMERIC,
    unit TEXT,
    CHECK (unit IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE AdBlueConsumption(
    name TEXT PRIMARY KEY,
    adBlueCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE FlocculantsConsumption(
    name TEXT PRIMARY KEY,
    flocculantsCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE SaltConsumption(
    name TEXT PRIMARY KEY,
    saltCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE PlasticsConsumption(
    name TEXT PRIMARY KEY,
    plasticsCon NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE UntreatedWoodWaste(
    name TEXT PRIMARY KEY,
    woodWaste NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE MunicipalSolidWaste(
    name TEXT PRIMARY KEY,
    MunSolidWaste NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE Inert_Waste_RockConsumption(
    name TEXT PRIMARY KEY,
    InertWaste NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE Hazardous_WasteConsumption(
    NAME TEXT PRIMARY KEY,
    HazardWaste NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE TABLE Mixed_WasteConsumption(
    NAME TEXT PRIMARY KEY,
    HazardWaste NUMERIC,
    unit TEXT,
    CHECK (UNIT IN ('m3', 'l', 'tonnes', 'kg')),
    FOREIGN KEY (name) REFERENCES InputMaterials(name)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

CREATE VIEW AllInformationConsumables AS (
    SELECT InputMaterials.name, 
    CONCAT(DieselConsumption.dieselCon,' ', DieselConsumption.unit) AS Diesel,
    CONCAT(ElectricityConsumption.electricityCon,' ', ElectricityConsumption.unit) AS Electricity,
    CONCAT(OilConsumption.oilConsumption,' ', OilConsumption.unit) AS Oil,
    CONCAT(WaterConsumption.waterCon,' ', WaterConsumption.unit) AS Water,
    CONCAT(ExplosivesConsumption.explosivesCon,' ', ExplosivesConsumption.unit) AS Explosives,
    CONCAT(MetalsConsumption.metalCon,' ', MetalsConsumption.unit) AS Metal,
    CONCAT(RubberConsumption.rubberCon,' ', RubberConsumption.unit) AS Rubber,
    CONCAT(ChemicalsConsumption.chemicalsCons,' ', ChemicalsConsumption.unit) AS Chemicals,
    CONCAT(AdBlueConsumption.adBlueCon,' ', AdBlueConsumption.unit) AS Adblue,
    CONCAT(FlocculantsConsumption.flocculantsCon,' ', FlocculantsConsumption.unit) AS Flocculants,
    CONCAT(SaltConsumption.saltCon,' ', SaltConsumption.unit) AS Salt,
    CONCAT(PlasticsConsumption.plasticsCon,' ', PlasticsConsumption.unit) AS Plastics,
    CONCAT(Inert_Waste_RockConsumption.InertWaste,' ', Inert_Waste_RockConsumption.unit) AS Inert_Waste_Rock,
    CONCAT(Hazardous_WasteConsumption.HazardWaste,' ', Hazardous_WasteConsumption.unit) AS Hazardous_Waste,
    CONCAT(Mixed_WasteConsumption.HazardWaste,' ', Mixed_WasteConsumption.unit) AS Mixed_Waste,
    totalProduction,
    totalExternal
    FROM InputMaterials
    JOIN DieselConsumption ON DieselConsumption.name = InputMaterials.name
    JOIN ElectricityConsumption ON ElectricityConsumption.name = InputMaterials.name
    JOIN WaterConsumption ON WaterConsumption.name = InputMaterials.name
    JOIN ExplosivesConsumption ON ExplosivesConsumption.name = InputMaterials.name
    JOIN OilConsumption ON OilConsumption.name = InputMaterials.name
    JOIN MetalsConsumption ON MetalsConsumption.name = InputMaterials.name
    JOIN RubberConsumption ON RubberConsumption.name = InputMaterials.name
    JOIN ChemicalsConsumption ON ChemicalsConsumption.name = InputMaterials.name
    JOIN AdBlueConsumption ON AdBlueConsumption.name = InputMaterials.name
    JOIN FlocculantsConsumption ON FlocculantsConsumption.name = InputMaterials.name
    JOIN SaltConsumption ON SaltConsumption.name = InputMaterials.name
    JOIN PlasticsConsumption ON PlasticsConsumption.name = InputMaterials.name
    JOIN Inert_Waste_RockConsumption ON Inert_Waste_RockConsumption.name = InputMaterials.name
    JOIN Hazardous_WasteConsumption ON Hazardous_WasteConsumption.name = InputMaterials.name
    JOIN Mixed_WasteConsumption ON Mixed_WasteConsumption.name = InputMaterials.name
    JOIN TotalYearlyProduction ON TotalYearlyProduction.name = InputMaterials.name
    JOIN TotalExternalMaterial ON TotalExternalMaterial.name = InputMaterials.name
)

DROP TABLE InputMaterials

DROP TABLE ProductionHours;
DROP TABLE MassFlowStockpiles;
DROP TABLE OilConsumption;
DROP TABLE ElectricityConsumption;
DROP TABLE ExplosivesConsumption;
DROP TABLE MetalsConsumption;
DROP TABLE totalBlastedMaterial;
DROP TABLE DieselConsumption;
DROP TABLE BulkDensity;
DROP TABLE TotalEntrepreneurMaterial;
DROP TABLE AdBlueConsumption;
DROP TABLE RubberConsumption;
DROP TABLE ChemicalsConsumption;
DROP TABLE FlocculantsConsumption;
DROP TABLE MoistureContent;
DROP TABLE SaltConsumption;
DROP TABLE PlasticsConsumption;
DROP TABLE UntreatedWoodWaste;
DROP TABLE WorkIndex;
DROP TABLE MunicipalSolidWaste;
DROP TABLE InertWasteRock;
DROP TABLE HazardousWaste;
DROP TABLE TotalYearlyProduction;
DROP TABLE WaterConsumption;
