CREATE TABLE bus (
	busID INT,
	destination VARCHAR(255) NOT NULL,
	seatcount INT NOT NULL,
	PRIMARY KEY(busID)
);
CREATE TABLE Seat (
	seatID SERIAL,
	width INT NOT NULL,
	height INT NOT NULL,
	reserved BOOL NOT NULL,
	busID INT NOT NULL,
	PRIMARY KEY(seatID),
	Constraint fk_bus FOREIGN KEY(busID) REFERENCES bus(busID)
);