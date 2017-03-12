/*The main database table to contain all Main information about a
  Character, skills, gear and cyberwear are contained in other tables
*/
DROP TABLE CP2020Db.CHARACTER_SKILLS;
DROP TABLE CP2020Db.CHARACTER_GEAR;
DROP TABLE CP2020Db.WEAPON_LIST;
DROP TABLE CP2020Db.SKILLS;
DROP TABLE CP2020Db.CP_CHARACTERS;
DROP TABLE CP2020Db.GEAR_LIST;

CREATE TABLE CP2020Db.CP_CHARACTERS(
            charID INTEGER NOT NULL AUTO_INCREMENT,
            charName VARCHAR(40) NOT NULL,
            charHandle VARCHAR(20),
            charPoints INTEGER NOT NULL,
            charRole VARCHAR(15) NOT NULL,
            charAge INTEGER NOT NULL,
            charAttract INTEGER NOT NULL,
            charBody INTEGER NOT NULL,
            charCool INTEGER NOT NULL, 
            charEmpathy INTEGER NOT NULL, 
            charIntelligence INTEGER NOT NULL,
            charLuck INTEGER NOT NULL,
            charMove INTEGER NOT NULL,
            charReflex INTEGER NOT NULL, 
            charTech INTEGER NOT NULL,
            charRun INTEGER NOT NULL,
            charLeap INTEGER NOT NULL,
            charLift INTEGER NOT NULL,
            charHumanity INTEGER NOT NULL,
            CONSTRAINT pk_characters PRIMARY KEY(charID)
            );        
            
/*A table that holds the information for the skills list
*/
CREATE TABLE CP2020Db.SKILLS(
          skillID INTEGER NOT NULL AUTO_INCREMENT,
          skillName VARCHAR(20) NOT NULL,
          skillType VARCHAR(20) NOT NULL,
          CONSTRAINT pk_skills PRIMARY KEY(skillID)
          );
          
CREATE TABLE CP2020Db.GEAR_LIST(
          gearID INTEGER NOT NULL AUTO_INCREMENT,
          gearName VARCHAR(40) NOT NULL,
          gearDescription VARCHAR(400),
          gearPrice INTEGER,
          gearWeight INTEGER NOT NULL,
          CONSTRAINT pk_gearlist PRIMARY KEY(gearID)
          );

CREATE TABLE CP2020Db.WEAPON_LIST(
          gearID INTEGER NOT NULL AUTO_INCREMENT,
          weaponDamage VARCHAR(20) NOT NULL,
          weaponType VARCHAR(20) NOT NULL,
          weaponRarity VARCHAR(20) NOT NULL,
          weaponRange VARCHAR(30),
          weaponFireRate INTEGER NOT NULL,
          CONSTRAINT pk_weaponlist PRIMARY KEY(gearID),
          CONSTRAINT fk_weaponlist FOREIGN KEY (gearID)
          REFERENCES CP2020Db.GEAR_LIST(gearID)
          );

CREATE TABLE CP2020Db.CHARACTER_SKILLS(
          characterID INTEGER NOT NULL,
          skillID INTEGER NOT NULL,
          skillLevel INTEGER NOT NULL,
          CONSTRAINT pk_charskill PRIMARY KEY(characterID, skillID), 
          CONSTRAINT fk_skillOwner FOREIGN KEY(characterID) 
            REFERENCES CP2020Db.CP_CHARACTERS(charID),
          CONSTRAINT fk_skillID FOREIGN KEY(skillID)
            REFERENCES CP2020Db.SKILLS(skillID)
          );
            
CREATE TABLE CP2020Db.CHARACTER_GEAR(
        gearID INTEGER NOT NULL AUTO_INCREMENT,
        characterID INTEGER NOT NULL,
        CONSTRAINT pk_chargear PRIMARY KEY(characterID, gearID),
        CONSTRAINT fk_gearOwner FOREIGN KEY(characterID)
          REFERENCES CP2020Db.CP_CHARACTERS(charID),
        CONSTRAINT fk_gear FOREIGN KEY(gearID)
          REFERENCES CP2020Db.GEAR_LIST(gearID)
        );
        
        
        