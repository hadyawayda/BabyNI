CREATE TABLE TRANS_MW_ERC_PM_JOIN AS 
        SELECT 
                T1.NETWORK_SID,
                T1.DATETIME_KEY,
                T1.NEID,
                T1."TIME" AS DATETIME_VALUE,
                T1.NEALIAS,
                T1.NETYPE,
                T1.MAXRXLEVEL,
                T2.RFINPUTPOWER,
                T1.LINK,
                T1.TID,
                T1.FARENDTID,
                T2.SLOT,
                T2.PORT
        FROM TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER T1
        JOIN TRANS_MW_ERC_PM_WAN_RFINPUTPOWER T2
        ON T1.NETWORK_SID = T2.NETWORK_SID
;

-- Add audit
INSERT INTO TRANS_MW_AGG_SLOT_HOURLY
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                NETWORK_SID,
                NEALIAS,
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_DAILY
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                NETWORK_SID,
                NEALIAS,
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_ALL_TIME
        SELECT
                DATETIME_KEY,
                NETWORK_SID,
                NEALIAS,
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
        ORDER BY
                NETWORK_SID,
                DATETIME_KEY,
                NEALIAS,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_HOURLY_NETYPE
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                DATETIME_KEY,
                NETYPE
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                DATETIME_KEY,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_HOURLY_NEALIAS
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                NEALIAS,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                DATETIME_KEY,
                NEALIAS
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 1, 'HOUR', 'END'),
                DATETIME_KEY,
                NEALIAS
;

INSERT INTO TRANS_MW_AGG_SLOT_DAILY_NETYPE
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                DATETIME_KEY,
                NETYPE
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                DATETIME_KEY,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_DAILY_NEALIAS
        SELECT
                DATETIME_KEY,
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                NEALIAS,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                DATETIME_KEY,
                NEALIAS
        ORDER BY 
                TIME_SLICE(DATETIME_VALUE, 24, 'HOUR', 'END'),
                DATETIME_KEY,
                NEALIAS
;

INSERT INTO TRANS_MW_AGG_SLOT_NETYPE
        SELECT
                DATETIME_KEY,
                NETYPE,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                DATETIME_KEY,
                NETYPE
        ORDER BY 
                DATETIME_KEY,
                NETYPE
;

INSERT INTO TRANS_MW_AGG_SLOT_NEALIAS
        SELECT
                DATETIME_KEY,
                NEALIAS,
                MAX(RFINPUTPOWER) AS RSL_INPUT_POWER,
                MAX(MAXRXLEVEL) AS MAX_RX_LEVEL,
                ROUND(ABS(MAX(RFINPUTPOWER) - MAX(MAXRXLEVEL)), 2)
        FROM    
                TRANS_MW_ERC_PM_JOIN
        GROUP BY
                DATETIME_KEY,
                NEALIAS
        ORDER BY
                DATETIME_KEY,
                NEALIAS
;

DROP TABLE IF EXISTS TRANS_MW_ERC_PM_JOIN;
DROP TABLE IF EXISTS TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER;
DROP TABLE IF EXISTS TRANS_MW_ERC_PM_WAN_RFINPUTPOWER;

CREATE TABLE TRANS_MW_ERC_PM_TN_RADIO_LINK_POWER (
        NETWORK_SID INTEGER,
        DATETIME_KEY INTEGER,
        NEID FLOAT,
        "OBJECT" VARCHAR,
        "TIME" DATETIME,
        "INTERVAL" INTEGER,
        DIRECTION VARCHAR,
        NEALIAS VARCHAR,
        NETYPE VARCHAR,
        RXLEVELBELOWTS1 VARCHAR,
        RXLEVELBELOWTS2 VARCHAR,
        MINRXLEVEL FLOAT,
        MAXRXLEVEL FLOAT,
        TXLEVELABOVETS1 VARCHAR,
        MINTXLEVEL FLOAT,
        MAXTXLEVEL FLOAT,
        FAILUREDESCRIPTION VARCHAR,
        LINK VARCHAR,
        TID VARCHAR,
        FARENDTID VARCHAR,
        SLOT VARCHAR,
        PORT VARCHAR
        )
        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1
        PARTITION BY time_slice(TIME, 1, 'HOUR', 'END')
        GROUP BY CASE
                WHEN DATEDIFF(HOUR, time_slice(TIME, 1, 'HOUR', 'END'), NOW()) >=24
                THEN DATE_TRUNC('DAY', time_slice(TIME, 1, 'HOUR', 'END'))
                ELSE time_slice(TIME, 1, 'HOUR', 'END')
                END;

CREATE TABLE TRANS_MW_ERC_PM_WAN_RFINPUTPOWER (
        NETWORK_SID INTEGER,
        DATETIME_KEY INTEGER,
        NODENAME VARCHAR,
        NEID FLOAT,
        "OBJECT" VARCHAR,
        "TIME" DATETIME,
        "INTERVAL" INTEGER,
        DIRECTION VARCHAR,
        NEALIAS VARCHAR,
        NETYPE VARCHAR,
        RFINPUTPOWER FLOAT,
        TID VARCHAR,
        FARENDTID VARCHAR,
        SLOT VARCHAR,
        PORT VARCHAR
        )
        SEGMENTED BY HASH(NETWORK_SID, DATETIME_KEY) ALL NODES KSAFE 1
        PARTITION BY time_slice(TIME, 24, 'HOUR', 'END')
        GROUP BY CASE
                WHEN DATEDIFF(DAY, time_slice(TIME, 24, 'HOUR', 'END'), NOW()) >=7
                THEN DATE_TRUNC('WEEK', time_slice(TIME, 24, 'HOUR', 'END'))
                ELSE time_slice(TIME, 24, 'HOUR', 'END')
                END;
