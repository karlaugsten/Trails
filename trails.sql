CREATE TABLE Trails (
    TrailId     INTEGER         PRIMARY KEY ASC AUTOINCREMENT
                                NOT NULL,
    Rating      DOUBLE          NOT NULL,
    MaxDuration DOUBLE          NOT NULL,
    MinDuration DOUBLE          NOT NULL,
    Description VARCHAR (10000) NOT NULL,
    Distance    DOUBLE          NOT NULL,
    Elevation   INT             NOT NULL,
    Title       VARCHAR (300)   NOT NULL,
    Location    VARCHAR (300)   NOT NULL,
    Approved    BOOLEAN         DEFAULT False
                                NOT NULL,
    EditId      INTEGER         REFERENCES TrailEdits (EditId) 
                                NOT NULL
);

CREATE TABLE Images (
    Id           INTEGER       PRIMARY KEY ASC AUTOINCREMENT,
    Name         VARCHAR (300) NOT NULL,
    Url          VARCHAR (500) NOT NULL,
    ThumbnailUrl VARCHAR (500) NOT NULL,
    TrailId      INTEGER       REFERENCES Trails (TrailId) 
                               NOT NULL,
    EditId       INTEGER       NOT NULL
                               REFERENCES TrailEdits (EditId) 
);


CREATE TABLE TrailEdits (
    EditId      INTEGER         PRIMARY KEY ASC AUTOINCREMENT
                                NOT NULL,
    Rating      DOUBLE          NOT NULL,
    MaxDuration DOUBLE          NOT NULL,
    MinDuration DOUBLE          NOT NULL,
    Description VARCHAR (10000) NOT NULL,
    Distance    DOUBLE          NOT NULL,
    Elevation   INT             NOT NULL,
    TrailId     INTEGER         REFERENCES Trails (TrailId),
    Title       VARCHAR (300)   NOT NULL,
    Location    VARCHAR (300)   NOT NULL
);