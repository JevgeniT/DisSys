create database "190653_softdelete"

USE "190653_softdelete"
GO
-- 
-- Table: PROPERTY
CREATE TABLE property (
      propertyId int  NOT NULL,
      name varchar(32)  NOT NULL,
      address varchar(32)  NOT NULL,    
      CONSTRAINT property_pk PRIMARY KEY  (propertyId)
);




-- Table: ROOM
CREATE TABLE room (
  roomId int  NOT NULL,
  name varchar(32)  NOT NULL,
  createdAt date not null ,
  deletedAt date,
  CONSTRAINT room_pk PRIMARY KEY  (roomId)
);


-- Table: PROPERTY_ROOMS                                                        
CREATE TABLE propertyRooms (
            propertyRoomsId int  NOT NULL,
            propertyId int  NOT NULL,
            roomId int  NOT NULL,
            price decimal NOT NULL ,
            currency varchar(3) not null ,
            CreatedAt date,
            DeletedAt date NULL,
            CONSTRAINT propertyRoomsId_pk PRIMARY KEY  (propertyRoomsId,roomId,CreatedAt),
            constraint FK_propertyId foreign key (propertyId) references property(propertyId),
            CONSTRAINT FK_roomId FOREIGN KEY (roomId) REFERENCES room(roomId)
);



insert into property (propertyId, name, address)
values (1,'Hilton','Kesklinn');

-- 
insert into room (roomId, name,createdAt,deletedAt  )
values (1,'Large','01/01/2020',null);
insert into room (roomId, name,createdAt,deletedAt  )
values (2,'Extra','01/01/2020',null);

insert into propertyRooms (propertyRoomsId, propertyId, roomId, price, currency, CreatedAt, DeletedAt)
values (1,1,1,55,'EUR','01/01/2020',null);
insert into propertyRooms (propertyRoomsId, propertyId, roomId, price, currency, CreatedAt, DeletedAt)
values (2,1,2,45,'EUR','01/01/2020',null);




--soft update 

insert into propertyRooms (propertyRoomsId, propertyId, roomId, price, currency, CreatedAt, DeletedAt)
select (select max(propertyRoomsId) +1 from propertyRooms) ,propertyId, roomId, price,currency,createdAt,deletedAt
from propertyRooms where propertyRoomsId =1 ;

declare @end date select @end = '02/02/2020';
update  propertyRooms
set  price  = (IIF(propertyRoomsId = 1, 66, propertyRooms.price)),                  
     CreatedAt=(IIF(propertyRoomsId = 1, @end, propertyRooms.CreatedAt)) ,               
     DeletedAt = ( case when propertyRoomsId = 3 then  @end end)
    where roomId = 1;
 


  

---Room update 
declare @end date select @end = '02/02/2020';

insert into room (roomId, name,CreatedAt, DeletedAt)
select (select max(roomId)+1 from room) ,name,createdAt,@end
from room where roomId =2 ;

update  room
set  name  = 'Extra Large',
     CreatedAt=@end
where roomId =2 ;



select propertyRoomsId, p.name, r2.name,price,currency,propertyRooms.CreatedAt, propertyRooms.DeletedAt
from propertyRooms
         join property p on propertyRooms.propertyId = p.propertyId
         join room r2 on propertyRooms.roomId = r2.roomId

