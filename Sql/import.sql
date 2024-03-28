-- Create table Dinesafe to import raw data
CREATE TABLE public."Dinesafe" (
	"_id" text NULL,
	"Rec #" text NULL,
	"Establishment ID" text NULL,
	"Inspection ID" text NULL,
	"Establishment Name" text NULL,
	"Establishment Type" text NULL,
	"Establishment Address" text NULL,
	"Establishment Status" text NULL,
	"Min. Inspections Per Year" text NULL,
	"Infraction Details" text NULL,
	"Inspection Date" text NULL,
	"Severity" text NULL,
	"Action" text NULL,
	"Outcome" text NULL,
	"Amount Fined" text NULL,
	"Latitude" text NULL,
	"Longitude" text NULL,
	"Hash" text NULL
);

/*
    Right now importing the data from the CSV file to the Dinesafe table using DBeaver.
    At some point COPY should be used.
    See: https://www.postgresql.org/docs/current/sql-copy.html
*/

-- Calulate hash for all rows
UPDATE public."Dinesafe" d SET "Hash" = md5(concat(d."Establishment ID",
	d."Inspection ID",
	d."Establishment Status",
	d."Infraction Details",
	d."Severity",
	d."Action",
	d."Outcome",
	d."Amount Fined"));

-- Import Establishments
MERGE INTO public."Establishments" AS e
USING (SELECT
			DISTINCT "Establishment ID",
			"Establishment Address",
			"Latitude",
			"Longitude",
			"Min. Inspections Per Year",
			"Establishment Name",
			"Establishment Type"
		FROM public."Dinesafe") AS d
ON e."Id" = CAST(d."Establishment ID" AS INTEGER)
WHEN NOT MATCHED THEN
	INSERT ("Id",
			"Address",
			"Latitude",
			"Longitude",
			"MinimumInspectionsPerYear",
			"Name",
			"Type")
	VALUES (CAST(d."Establishment ID" AS INTEGER),
			d."Establishment Address",
			CAST(d."Latitude" AS FLOAT),
			CAST(d."Longitude" AS FLOAT),
			d."Min. Inspections Per Year",
			d."Establishment Name",
			d."Establishment Type");

-- Import Inspections
MERGE INTO public."Inspections" AS i
USING (SELECT
			DISTINCT "Inspection ID",
			"Inspection Date",
			"Establishment ID"
		FROM public."Dinesafe"
		WHERE "Inspection ID" <> '') AS d
ON i."Id" = CAST(d."Inspection ID" AS INTEGER)
WHEN NOT MATCHED THEN
	INSERT ("Id",
			"Date",
			"EstablishmentId")
	VALUES (CAST(d."Inspection ID" AS INTEGER),
			TO_DATE(d."Inspection Date", 'YYYY-MM-DD'),
			CAST(d."Establishment ID" AS INTEGER));

-- Inspections details
MERGE INTO public."InspectionDetails" AS id
USING (SELECT
			"Hash",
			"Action",
			"Amount Fined",
			"Outcome",
			"Infraction Details",
			"Inspection ID",
			"Severity",
			"Establishment Status"
		FROM public."Dinesafe" t
		WHERE "Inspection ID" <> '') AS d
ON id."Id" = d."Hash"
WHEN NOT MATCHED then
	INSERT ("Id",
			"Action",
			"AmountFined",
			"CourtOutcome",
			"InfractionDetails",
			"InspectionId",
			"Severity",
			"Status")
	values (d."Hash",
			d."Action",
			CAST(
				CASE
					WHEN d."Amount Fined" = '' THEN NULL
					ELSE d."Amount Fined"
				END
				AS NUMERIC),
			d."Outcome",
			d."Infraction Details",
			CAST(d."Inspection ID" AS INTEGER),
            CASE
                WHEN d."Severity" = 'NA - Not Applicable' THEN 'NotApplicable'
                WHEN d."Severity" = 'M - Minor' THEN 'Minor'
                WHEN d."Severity" = 'S - Significant' THEN 'Significant'
                WHEN d."Severity" = 'C - Crucial' THEN 'Crucial'
                ELSE NULL
            END,
            CASE
                WHEN d."Establishment Status" = 'Pass' THEN 'Pass'
                WHEN d."Establishment Status" = 'Conditional Pass' THEN 'ConditionalPass'
                WHEN d."Establishment Status" = 'Closed' THEN 'Closed'
            END);

-- Drop table
DROP TABLE public."Dinesafe";
