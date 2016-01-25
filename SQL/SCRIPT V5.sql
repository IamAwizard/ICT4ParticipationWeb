	--ICT4Participation SQL - Tweede periode
	
	--Drop tables
	drop table TGEBRUIKER 					cascade constraints;
	drop table TACCOUNT 					cascade constraints;
	drop table TBEHEERDER 					cascade constraints;
	drop table THULPBEHOEVENDE 				cascade constraints;
	drop table TVRIJWILLIGER 				cascade constraints;
	drop table TVRIJWILLIGER_VAARDIGHEID	cascade constraints;
	drop table TCHAT 						cascade constraints;
	drop table TBESCHIKBAARHEID 			cascade constraints;
	drop table THULPVRAAG 					cascade constraints;
	drop table THULPVRAAG_VAARDIGHEID		cascade constraints;
	drop table THULPVRAAG_VRIJWILLIGER		cascade constraints;
	drop table TREVIEW 						cascade constraints;
	drop table TVAARDIGHEID 				cascade constraints;
	drop table TVERVOER 					cascade constraints;
	drop table TAFSPRAAK					cascade constraints;
	
	--Drop sequences
	drop sequence   seq_ACCOUNT;
	drop sequence	seq_GEBRUIKER;
	drop sequence   seq_BEHEERDER;
	drop sequence	seq_HULPBEHOEVENDE;
	drop sequence	seq_VRIJWILLIGER;
	drop sequence	seq_CHAT;
	drop sequence	seq_BESCHIKBAARHEID;
	drop sequence	seq_HULPVRAAG;
	drop sequence	seq_REVIEW;
	drop sequence	seq_VAARDIGHEID;
	drop sequence	seq_VERVOER;
	drop sequence 	seq_AFSPRAAK;
	
	-- Drop procedures
	drop procedure vul_Beschikbaarheid;
	
	--Create tables
	CREATE TABLE TACCOUNT (
	id						number(7) 		primary key,
	gebruikersnaam			varchar2(30)	not null,
	wachtwoord				varchar2(50)	not null,
	email					varchar2(50)	not null,
	
	constraint chk_TACCOUNT_email check(email LIKE '%@%')
	);
	
	
	CREATE TABLE TGEBRUIKER (
	id						number(7)		primary key,
	naam					varchar2(30)	not null,
	adres					varchar2(50)	not null,
	woonplaats				varchar2(30)	not null,
	telefoonnummer			varchar(10)		not null,
	heeftrijbewijs			varchar2(5)		default 'False',
	heeftauto				varchar2(5)		default 'False',
	uitschrijvingsdatum		date			,
	accountid				number(7)		not null,
	
	constraint fk_TGEBRUIKER_accountid foreign key(accountid)REFERENCES TACCOUNT(id) on delete cascade,
	constraint chk_TGEBRUIKER_heeftrijbewijs check(UPPER(heeftrijbewijs) IN ('TRUE', 'FALSE')),
	constraint chk_TGEBRUIKER_heeftauto check(UPPER(heeftauto) IN ('TRUE', 'FALSE'))
	);
	
	
	CREATE TABLE TBEHEERDER (
	id						number(7)		primary key,
	accountid				number(7)		not null,
	
	constraint fk_TBEHEERDER_accountid foreign key(accountid)REFERENCES TACCOUNT(id) on delete cascade
	);
	
	
	CREATE TABLE THULPBEHOEVENDE (
	id						number(7)		primary key,
	OVMogelijk				varchar2(5)		default 'False',
	gebruikerid				number(7)		not null,
	
	constraint fk_THULP_gebruikerid	foreign key(gebruikerid)REFERENCES TGEBRUIKER(id) on delete cascade,
	constraint chk_TGEBRUIKER_OVMogelijk check(UPPER(OVMogelijk) IN ('TRUE', 'FALSE'))
	);
	
	
	CREATE TABLE TVAARDIGHEID (
	id						number(7)		primary key,
	omschrijving			varchar2(255)	not null
	);
	
	
	CREATE TABLE TVRIJWILLIGER (
	id						number(7)		primary key,
	geboortedatum			date			not null,
	foto					varchar2(255)	not null,
	vog						varchar2(255)	not null,
	gebruikerid				number(7)		not null,
	
	constraint fk_TVRIJWILLIGER_gebruikerid	foreign key(gebruikerid)REFERENCES TGEBRUIKER(id) on delete cascade
	);
	
	
	CREATE TABLE TCHAT (
	id						number(7)		primary key,
	tijdstip				date			not null,
	bericht					varchar2(255)	not null,
	hulpbehoevendeid		number(7)		not null,
	vrijwilligerid			number(7)		not null,
	vanhulpbehoevende		number(1)		not null,
	
	constraint fk_TCHAT_hulpid	foreign key(hulpbehoevendeid)REFERENCES THULPBEHOEVENDE(id) on delete cascade,
	constraint fk_TCHAT_vrijwid foreign key(vrijwilligerid)REFERENCES TVRIJWILLIGER(id) on delete cascade
	);
	
	
	CREATE TABLE TBESCHIKBAARHEID (
	id						number(7)		primary key,
	dagnaam					varchar2(20)	not null,
	dagdeel					varchar2(20)	not null,
	vrijwilligerid			number(7)		not null,
	
	constraint chk_TBESCHIKBAARHEID_dagnaam check(UPPER(dagnaam) IN ('MAANDAG', 'DINSDAG', 'WOENSDAG', 'DONDERDAG', 'VRIJDAG', 'ZATERDAG', 'ZONDAG')),
	constraint chk_TBESCHIKBAARHEID_dagdeel check(UPPER(dagdeel) IN ('NIET BESCHIKBAAR','OCHTEND', 'MIDDAG', 'NAMIDDAG', 'AVOND', 'NACHT')),
	constraint fk_TBESCHIKBAARHEID_vrijwid foreign key(vrijwilligerid) REFERENCES TVRIJWILLIGER(id) on delete cascade
	);
	
	
	CREATE TABLE TVERVOER (
	id						number(7)		primary key,
	omschrijving			varchar2(255)	not null
	);
	
	
	CREATE TABLE THULPVRAAG (
	id						number(7)		primary key,
	omschrijving			varchar2(3000)	not null,
	locatie					varchar2(255)	,
	reistijd				varchar2(255)	,
	startdatum				date			not null,
	einddatum				date			,
	urgent					varchar2(5)		default 'False',
	aantalvrijwilligers		number(5)		not null,
	auteur					number(7)		not null,
	vervoertype				number(7)		,
	
	constraint chk_TGEBRUIKER_urgent check(UPPER(urgent) IN ('TRUE', 'FALSE')),
	constraint fk_THULPVRAAG_vervoertype foreign key(vervoertype) REFERENCES TVERVOER(id) on delete cascade,
	constraint fk_THULPVRAAG_hulpbehoevende foreign key(auteur) REFERENCES THULPBEHOEVENDE(id) on delete cascade
	);
	
	
	CREATE TABLE TVRIJWILLIGER_VAARDIGHEID
	(
	vrijwilligerid			number(7), 
	vaardigheidid			number(7),
	
	constraint check_VRIJVAAR_pk primary key(vrijwilligerid, vaardigheidid),
	constraint fk_VRIJVAAR_vrijwilligerid foreign key(vrijwilligerid) REFERENCES TVRIJWILLIGER(id) on delete cascade,
	constraint fk_VRIJVAAR_vaardigheidid foreign key(vaardigheidid) REFERENCES TVAARDIGHEID(id) on delete cascade
	);
	
	
	CREATE TABLE THULPVRAAG_VAARDIGHEID
	(
	hulpvraagid				number(7), 
	vaardigheidid			number(7),
	
	constraint check_HULPVAAR_pk primary key(hulpvraagid, vaardigheidid),
	constraint fk_HULPVAAR_hulpvraagid foreign key(hulpvraagid) REFERENCES THULPVRAAG(id) on delete cascade,
	constraint fk_HULPVAAR_vaardigheidid foreign key(vaardigheidid) REFERENCES TVAARDIGHEID(id) on delete cascade
	);
	
	
	CREATE TABLE THULPVRAAG_VRIJWILLIGER
	(
	hulpvraagid				number(7), 
	vrijwilligerid			number(7),
	
	constraint check_HULPVRIJ_pk primary key(hulpvraagid, vrijwilligerid),
	constraint fk_HULPVRIJ_hulpvraagid foreign key(hulpvraagid) REFERENCES THULPVRAAG(id) on delete cascade,
	constraint fk_HULPVRIJ_vrijwilligerid foreign key(vrijwilligerid) REFERENCES TVRIJWILLIGER(id) on delete cascade
	);
	

	CREATE TABLE TREVIEW (
	id						number(7)		primary key,
	beoordeling				number(1)		not null,
	opmerkingen				varchar2(255)	not null,
	vrijwilligerid			number(7)		not null,
	hulpvraagid				number(7)		not null,
	
	constraint chk_TREVIEW_beoordeling check(beoordeling IN (1, 2, 3, 4, 5)),
	constraint fk_TREVIEW_vrijwilligerid foreign key(vrijwilligerid)REFERENCES TVRIJWILLIGER(id) on delete cascade,
	constraint fk_TREVIEW_hulpvraagid foreign key(hulpvraagid)REFERENCES THULPVRAAG(id) on delete cascade
	);
	
	CREATE TABLE TAFSPRAAK (
	id						number(7)		primary key,
	hulpbehoevendeid		number(7)		not null,
	vrijwilligerid			number(7)		not null,
	datum					date			not null,
	locatie					varchar2(150)	not null,
	
	constraint fk_TAFSPRAAK_hulpbehoevendeid foreign key(hulpbehoevendeid)REFERENCES THULPBEHOEVENDE(id) on delete cascade,
	constraint fk_TAFSPRAAK_vrijwilligerid foreign key(vrijwilligerid)REFERENCES TVRIJWILLIGER(id) on delete cascade
	);
	
	--increment
		--TGEBRUIKER
	CREATE Sequence seq_ACCOUNT
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_ACCOUNT
	before insert on TACCOUNT
	for each row
	begin
	select seq_ACCOUNT.nextval into :new.id from dual;
	end;
	/
	
	--TGEBRUIKER
	CREATE Sequence seq_GEBRUIKER
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_GEBRUIKER
	before insert on TGEBRUIKER
	for each row
	begin
	select seq_GEBRUIKER.nextval into :new.id from dual;
	end;
	/
	
	--TBEHEERDER
	CREATE Sequence seq_BEHEERDER
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_BEHEERDER
	before insert on TBEHEERDER
	for each row
	begin
	select seq_BEHEERDER.nextval into :new.id from dual;
	end;
	/
	
	--THULPBEHOEVENDE
	CREATE Sequence seq_HULPBEHOEVENDE
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_HULPBEHOEVENDE
	before insert on THULPBEHOEVENDE
	for each row
	begin
	select seq_HULPBEHOEVENDE.nextval into :new.id from dual;
	end;
	/
	
	--TVRIJWILLIGER
	CREATE Sequence seq_VRIJWILLIGER
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_VRIJWILLIGER
	before insert on TVRIJWILLIGER
	for each row
	begin
	select seq_VRIJWILLIGER.nextval into :new.id from dual;
	end;
	/
	
	--TCHAT
	CREATE Sequence seq_CHAT
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_CHAT
	before insert on TCHAT
	for each row
	begin
	select seq_CHAT.nextval into :new.id from dual;
	end;
	/
	
	--TBESCHIKBAARHEID
	CREATE Sequence seq_BESCHIKBAARHEID
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_BESCHIKBAARHEID
	before insert on TBESCHIKBAARHEID
	for each row
	begin
	select seq_BESCHIKBAARHEID.nextval into :new.id from dual;
	end;
	/
	
	--THULPVRAAG
	CREATE Sequence seq_HULPVRAAG
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_HULPVRAAG
	before insert on THULPVRAAG
	for each row
	begin
	select seq_HULPVRAAG.nextval into :new.id from dual;
	end;
	/
	
	--TREVIEW
	CREATE Sequence seq_REVIEW
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_REVIEW
	before insert on TREVIEW
	for each row
	begin
	select seq_REVIEW.nextval into :new.id from dual;
	end;
	/
	
	--TVAARDIGHEID
	CREATE Sequence seq_VAARDIGHEID
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_VAARDIGHEID
	before insert on TVAARDIGHEID
	for each row
	begin
	select seq_VAARDIGHEID.nextval into :new.id from dual;
	end;
	/
	
	--TVERVOER
	CREATE Sequence seq_VERVOER
	minvalue 1
	start with 1
	increment by 1
	cache 10;

	CREATE Trigger trigger_VERVOER
	before insert on TVERVOER
	for each row
	begin
	select seq_VERVOER.nextval into :new.id from dual;
	end;
	/
	
	--TAFSPRAAK
	CREATE Sequence seq_AFSPRAAK
	minvalue 1
	start with 1
	increment by  1
	cache 10;
	
	CREATE Trigger trigger_AFSPRAAK
	before insert on TAFSPRAAK
	for each row
	begin
	select seq_AFSPRAAK.nextval into :new.id from dual;
	end;
	/
	
	COMMIT;
	-- Procedures
	-- Een procedure om de beschikbaarheiden standaard in te stellen
	CREATE OR REPLACE PROCEDURE vul_Beschikbaarheid(
	  p_id IN INTEGER
	)
	IS
	BEGIN
	  DELETE FROM TBESCHIKBAARHEID WHERE VRIJWILLIGERID = p_id;

	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Maandag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Dinsdag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Woensdag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Donderdag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Vrijdag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Zaterdag', 'Niet Beschikbaar', p_id);
	  INSERT INTO TBESCHIKBAARHEID(DAGNAAM, DAGDEEL, VRIJWILLIGERID) VALUES ('Zondag', 'Niet Beschikbaar', p_id);
	END;
	/

	create or replace trigger Trigger_AUTOBESCHIKBAARHEID
	after insert on TVRIJWILLIGER
	for each row
	begin
	   vul_Beschikbaarheid(:new.ID);
	end;
	/

COMMIT;

INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES('youri@email.nl', 'youri', 'youritje123');
INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES('merijn@email.nl', 'merijn', 'merijntje123');
INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES('dundy@email.nl', 'dundy', 'dundytje123');
INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES('maurice@email.nl', 'maurice', 'mauriceje123');
INSERT INTO TACCOUNT(EMAIL, WACHTWOORD, GEBRUIKERSNAAM) VALUES('jeroen@email.nl', 'jeroen', 'jeroentje123');

INSERT INTO TGEBRUIKER(NAAM,ADRES,WOONPLAATS,TELEFOONNUMMER,HEEFTRIJBEWIJS,HEEFTAUTO,UITSCHRIJVINGSDATUM,ACCOUNTID) VALUES ('Youri van der Ceelen', 'Tongelresestraat 4f', 'Eindhoven','0640219874','True','True',NULL,1);
INSERT INTO TGEBRUIKER(NAAM,ADRES,WOONPLAATS,TELEFOONNUMMER,HEEFTRIJBEWIJS,HEEFTAUTO,UITSCHRIJVINGSDATUM,ACCOUNTID) VALUES ('Merijn den Oudsten', 'merijnstraat 5', 'Lopik','0123456789','False','True',NULL,2);
INSERT INTO TGEBRUIKER(NAAM,ADRES,WOONPLAATS,TELEFOONNUMMER,HEEFTRIJBEWIJS,HEEFTAUTO,UITSCHRIJVINGSDATUM,ACCOUNTID) VALUES ('Dundy Pura', 'Puralaanstraatweg 12', 'Helmond','0987654321','True','False',NULL,3);
INSERT INTO TGEBRUIKER(NAAM,ADRES,WOONPLAATS,TELEFOONNUMMER,HEEFTRIJBEWIJS,HEEFTAUTO,UITSCHRIJVINGSDATUM,ACCOUNTID) VALUES ('Maurice Harsveld', '''t Veld 3', 'Neunen','1234567890','False','False',NULL,4);

INSERT INTO TVRIJWILLIGER(GEBOORTEDATUM, FOTO, VOG, GEBRUIKERID) VALUES(TO_DATE('23-12-1996','dd-mm-yyyy'), '//profielfoto.png', '//VOG.pdf', 3);
INSERT INTO TVRIJWILLIGER(GEBOORTEDATUM, FOTO, VOG, GEBRUIKERID) VALUES(TO_DATE('11-02-1993','dd-mm-yyyy'), '//profielfoto.png', '//VOG.pdf', 4);

INSERT INTO THULPBEHOEVENDE(OVMOGELIJK,GEBRUIKERID) VALUES ('True',1);
INSERT INTO THULPBEHOEVENDE(OVMOGELIJK,GEBRUIKERID) VALUES ('False',2);

INSERT INTO TAFSPRAAK(HULPBEHOEVENDEID,VRIJWILLIGERID,DATUM,LOCATIE) VALUES(1,1,TO_DATE('23-12-2016', 'dd-mm-yyyy'),'Eindhoven');
INSERT INTO TAFSPRAAK(HULPBEHOEVENDEID,VRIJWILLIGERID,DATUM,LOCATIE) VALUES(2,2,TO_DATE('21-11-2016', 'dd-mm-yyyy'),'Helmond');

INSERT INTO TBEHEERDER(ACCOUNTID) VALUES(5);

INSERT INTO TCHAT(BERICHT,HULPBEHOEVENDEID,VRIJWILLIGERID, TIJDSTIP, VANHULPBEHOEVENDE) VALUES ('HALLO IK BEN BERICHT 1, 1 in dit gesprek',1,2, TO_DATE('11/01/2016 13:21:12', 'MM/dd/yyyy HH24:mi:ss'), 1);
INSERT INTO TCHAT(BERICHT,HULPBEHOEVENDEID,VRIJWILLIGERID, TIJDSTIP, VANHULPBEHOEVENDE) VALUES ('HALLO IK BEN BERICHT 2, 1 in dit gesprek',2,1, TO_DATE('11/01/2016 13:21:43', 'MM/dd/yyyy HH24:mi:ss'), 1);
INSERT INTO TCHAT(BERICHT,HULPBEHOEVENDEID,VRIJWILLIGERID, TIJDSTIP, VANHULPBEHOEVENDE) VALUES ('HALLO IK BEN BERICHT 3, 2 in dit gesprek',1,2, TO_DATE('11/01/2016 14:40:53', 'MM/dd/yyyy HH24:mi:ss'), 1);
INSERT INTO TCHAT(BERICHT,HULPBEHOEVENDEID,VRIJWILLIGERID, TIJDSTIP, VANHULPBEHOEVENDE) VALUES ('HALLO IK BEN BERICHT 4, 2 in dit gesprek',2,1, TO_DATE('12/01/2016 09:22:21', 'MM/dd/yyyy HH24:mi:ss'), 0);

INSERT INTO TVERVOER(OMSCHRIJVING) VALUES('Auto');
INSERT INTO TVERVOER(OMSCHRIJVING) VALUES('Fiets');
INSERT INTO TVERVOER(OMSCHRIJVING) VALUES('Openbaar Vervoer');
INSERT INTO TVERVOER(OMSCHRIJVING) VALUES('Te voet');

INSERT INTO THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE, AUTEUR) VALUES('Hallo ik heb hulp nodig met mijn rollator', 'Eindhoven', '2 minuten', TO_DATE('11-11-2015','dd-mm-yyyy'), NULL, 'True', 0, 1, 1);
INSERT INTO THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE, AUTEUR) VALUES('HALLO MIJN CAPS LOCK TOETS ZIT VAST', 'Helmond', '20 minuten', TO_DATE('01-01-2016','dd-mm-yyyy'), NULL, 'False', 0, 2, 1);
INSERT INTO THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE, AUTEUR) VALUES('mijn auto is kapot, is er iemand die deze goedkoop kan repareren?', 'Utrecht', '2 uur', TO_DATE('11-11-2015','dd-mm-yyyy'), NULL, 'True', 0, 3, 2);
INSERT INTO THULPVRAAG(OMSCHRIJVING, LOCATIE, REISTIJD, STARTDATUM, EINDDATUM, URGENT, AANTALVRIJWILLIGERS, VERVOERTYPE, AUTEUR) VALUES('Deze vraag zou je niet moeten zien als vrijwilliger.', 'Utrecht', '2 uur', TO_DATE('11-11-2015','dd-mm-yyyy'), TO_DATE('12-11-2015','dd-mm-yyyy'), 'True', 0, 3, 2);

INSERT INTO TVAARDIGHEID(OMSCHRIJVING) VALUES('Geduldig');
INSERT INTO TVAARDIGHEID(OMSCHRIJVING) VALUES('Verzorgend');

INSERT INTO THULPVRAAG_VAARDIGHEID(HULPVRAAGID, VAARDIGHEIDID) VALUES(1,1);
INSERT INTO THULPVRAAG_VAARDIGHEID(HULPVRAAGID, VAARDIGHEIDID) VALUES(2,2);

INSERT INTO THULPVRAAG_VRIJWILLIGER(HULPVRAAGID, VRIJWILLIGERID) VALUES(1,1);
INSERT INTO THULPVRAAG_VRIJWILLIGER(HULPVRAAGID, VRIJWILLIGERID) VALUES(2,2);

INSERT INTO TREVIEW(BEOORDELING,OPMERKINGEN,VRIJWILLIGERID,HULPVRAAGID) VALUES(3,'Reageert een beetje laat',1,1);
INSERT INTO TREVIEW(BEOORDELING,OPMERKINGEN,VRIJWILLIGERID,HULPVRAAGID) VALUES(5,'Geen opmerkingen, alles was goed!',2,2);

INSERT INTO TVRIJWILLIGER_VAARDIGHEID(VRIJWILLIGERID, VAARDIGHEIDID) VALUES(1,1);
INSERT INTO TVRIJWILLIGER_VAARDIGHEID(VRIJWILLIGERID, VAARDIGHEIDID) VALUES(2,2);

COMMIT;

