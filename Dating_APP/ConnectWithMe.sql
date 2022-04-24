-- Script Date: 12/18/2021 7:20 PM  - ErikEJ.SqlCeScripting version 3.5.2.90
-- Database information:
-- Database: C:\Users\Lenovo\source\repos\Dating_APP\Dating_APP\datingapp.db
-- ServerVersion: 3.35.5
-- DatabaseSize: 156 KB
-- Created: 10/28/2021 12:54 PM

-- User Table information:
-- Number of tables: 13
-- __EFMigrationsHistory: -1 row(s)
-- AspNetRoleClaims: -1 row(s)
-- AspNetRoles: -1 row(s)
-- AspNetUserClaims: -1 row(s)
-- AspNetUserLogins: -1 row(s)
-- AspNetUserRoles: -1 row(s)
-- AspNetUsers: -1 row(s)
-- AspNetUserTokens: -1 row(s)
-- Connections: -1 row(s)
-- Groups: -1 row(s)
-- Likes: -1 row(s)
-- Messages: -1 row(s)
-- Photos: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Groups] (
  [Name] text NOT NULL
, CONSTRAINT [sqlite_autoindex_Groups_1] PRIMARY KEY ([Name])
);
CREATE TABLE [Connections] (
  [ConnectionId] text NOT NULL
, [Username] text NULL
, [GroupName] text NULL
, CONSTRAINT [sqlite_autoindex_Connections_1] PRIMARY KEY ([ConnectionId])
, CONSTRAINT [FK_Connections_0_0] FOREIGN KEY ([GroupName]) REFERENCES [Groups] ([Name]) ON DELETE RESTRICT ON UPDATE NO ACTION
);
CREATE TABLE [AspNetUsers] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [AccessFailedCount] bigint NOT NULL
, [City] text NULL
, [ConcurrencyStamp] text NULL
, [Country] text NULL
, [Created] text NOT NULL
, [DateOfBirth] text NOT NULL
, [Email] text NULL
, [EmailConfirmed] bigint NOT NULL
, [Gender] text NULL
, [Interests] text NULL
, [Introduction] text NULL
, [KnownAs] text NULL
, [LastActive] text NOT NULL
, [LockoutEnabled] bigint NOT NULL
, [LockoutEnd] text NULL
, [LookingFor] text NULL
, [NormalizedEmail] text NULL
, [NormalizedUserName] text NULL
, [PasswordHash] text NULL
, [PhoneNumber] text NULL
, [PhoneNumberConfirmed] bigint NOT NULL
, [SecurityStamp] text NULL
, [TwoFactorEnabled] bigint NOT NULL
, [UserName] text NULL
);
CREATE TABLE [Photos] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [AppUserId] bigint NOT NULL
, [IsMain] bigint NOT NULL
, [PublicId] text NULL
, [Url] text NULL
, [IsApproved] bigint DEFAULT (0) NOT NULL
, CONSTRAINT [FK_Photos_0_0] FOREIGN KEY ([AppUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [Messages] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Content] text NULL
, [DateRead] text NULL
, [MessageSent] text NOT NULL
, [RecipientDeleted] bigint NOT NULL
, [RecipientId] bigint NOT NULL
, [RecipientUsername] text NULL
, [SenderDeleted] bigint NOT NULL
, [SenderId] bigint NOT NULL
, [SenderUsername] text NULL
, CONSTRAINT [FK_Messages_0_0] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE RESTRICT ON UPDATE NO ACTION
, CONSTRAINT [FK_Messages_1_0] FOREIGN KEY ([RecipientId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE RESTRICT ON UPDATE NO ACTION
);
CREATE TABLE [Likes] (
  [SourceUserId] INTEGER NOT NULL
, [LikeUserId] INTEGER NOT NULL
, CONSTRAINT [sqlite_autoindex_Likes_1] PRIMARY KEY ([SourceUserId],[LikeUserId])
, CONSTRAINT [FK_Likes_0_0] FOREIGN KEY ([SourceUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
, CONSTRAINT [FK_Likes_1_0] FOREIGN KEY ([LikeUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [AspNetUserTokens] (
  [UserId] INTEGER NOT NULL
, [LoginProvider] text NOT NULL
, [Name] text NOT NULL
, [Value] text NULL
, CONSTRAINT [sqlite_autoindex_AspNetUserTokens_1] PRIMARY KEY ([UserId],[LoginProvider],[Name])
, CONSTRAINT [FK_AspNetUserTokens_0_0] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [AspNetUserLogins] (
  [LoginProvider] text NOT NULL
, [ProviderKey] text NOT NULL
, [ProviderDisplayName] text NULL
, [UserId] bigint NOT NULL
, CONSTRAINT [sqlite_autoindex_AspNetUserLogins_1] PRIMARY KEY ([LoginProvider],[ProviderKey])
, CONSTRAINT [FK_AspNetUserLogins_0_0] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [AspNetUserClaims] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [UserId] bigint NOT NULL
, [ClaimType] text NULL
, [ClaimValue] text NULL
, CONSTRAINT [FK_AspNetUserClaims_0_0] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [AspNetRoles] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [Name] text NULL
, [NormalizedName] text NULL
, [ConcurrencyStamp] text NULL
);
CREATE TABLE [AspNetUserRoles] (
  [UserId] INTEGER NOT NULL
, [RoleId] INTEGER NOT NULL
, CONSTRAINT [sqlite_autoindex_AspNetUserRoles_1] PRIMARY KEY ([UserId],[RoleId])
, CONSTRAINT [FK_AspNetUserRoles_0_0] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
, CONSTRAINT [FK_AspNetUserRoles_1_0] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [AspNetRoleClaims] (
  [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [RoleId] bigint NOT NULL
, [ClaimType] text NULL
, [ClaimValue] text NULL
, CONSTRAINT [FK_AspNetRoleClaims_0_0] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
);
CREATE TABLE [__EFMigrationsHistory] (
  [MigrationId] text NOT NULL
, [ProductVersion] text NOT NULL
, CONSTRAINT [sqlite_autoindex___EFMigrationsHistory_1] PRIMARY KEY ([MigrationId])
);
INSERT INTO [Groups] ([Name]) VALUES (
'eliza-genti');
INSERT INTO [Groups] ([Name]) VALUES (
'eliza-gg');
INSERT INTO [Groups] ([Name]) VALUES (
'eliza-warner');
INSERT INTO [Groups] ([Name]) VALUES (
'elizagenti');
INSERT INTO [Groups] ([Name]) VALUES (
'elizawarner');
INSERT INTO [Groups] ([Name]) VALUES (
'sherri-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'1z5Tejkude7itDZy6FKyKg','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'3gjguOw8kdjFKqva0aoxiw','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'7p_LahOEhKuVRiI0eFCbjw','eliza','elizawarner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'QO4mWFuqBNUcUBLXhBt2yA','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'Rw9AMaxySX3rH6A8ZRXGAg','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'Sx9ANAUlGLTagKhryXvgGA','genti','eliza-genti');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'd6uAwxojaNoTgK5eF6IusQ','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'lyvymALlA-Tk-XHmr4pIrA','eliza','elizawarner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'nrhqH4A7heUbElwyv8FskA','eliza','elizawarner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'rfwGxgz48iK5aC1xAZCxlQ','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'sNJ6V8JgJUG033Y-fhnabg','warner','eliza-warner');
INSERT INTO [Connections] ([ConnectionId],[Username],[GroupName]) VALUES (
'xOtKZXSfIAeuyf-IvT_-rA','eliza','elizawarner');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
1,0,'Siglerville','36ab8ea6-be2d-48cc-8cf6-d28e404870b5','Singapore','2021-07-27 00:00:00','1970-04-22 00:00:00',NULL,0,'female','Adipisicing veniam sit consequat aliquip.','Sit voluptate cillum adipisicing duis anim aliqua sit officia reprehenderit reprehenderit magna commodo id et. Lorem commodo reprehenderit eiusmod sunt voluptate commodo irure mollit. Amet dolore proident eiusmod est voluptate nisi.
','Sherri','2021-10-28 13:44:55.2203275',1,NULL,'Anim nulla ea labore amet ex dolore nulla id. Sint sit id irure fugiat minim reprehenderit labore reprehenderit enim ad ullamco nulla. Nulla ad minim incididunt est enim magna deserunt proident. Quis laboris nostrud ipsum velit cupidatat cupidatat qui. Dolore quis eiusmod ex pariatur. Amet eiusmod veniam velit ut. Ullamco amet aliqua ut quis.
',NULL,'SHERRI','AQAAAAEAACcQAAAAEEJfCy2FSQIyqk3RokSa+MNHFD3xrdGPvn4W7eCB1mWreYX4S6w2Zxcd9xHKfhBgng==',NULL,0,'QEA3DOTNOKU3JXXPKEWN4IOF3B2S2RJM',0,'sherri');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
2,0,'Wells','0224c325-7057-4845-b2a6-b747097bd3ae','Somalia','2021-04-05 00:00:00','1963-06-10 00:00:00',NULL,0,'female','Elit laborum commodo aliquip mollit sint laborum aliqua quis voluptate.','Nulla Lorem ut anim irure pariatur sunt do occaecat nostrud aliqua eu consequat consectetur non. Non eiusmod exercitation ad ut aute minim nulla sunt. Laborum esse fugiat labore ex duis qui dolore fugiat magna id veniam do ex. Elit exercitation commodo dolore sint aliquip non enim. Reprehenderit non nisi laborum elit excepteur id aliqua laboris aliqua. Sunt amet ullamco minim nisi in tempor et eiusmod non. Eiusmod fugiat non tempor incididunt in laborum consequat ullamco reprehenderit irure sit incididunt est.
','Sheree','2021-06-30 00:00:00',1,NULL,'Aliquip velit quis nisi minim voluptate nostrud ipsum. Culpa ad exercitation Lorem elit sit adipisicing irure voluptate enim velit deserunt. Qui veniam elit mollit laborum et aliquip. Ad nostrud dolore proident nostrud deserunt in sit. Est sint sint elit amet in deserunt non consequat ipsum id ex adipisicing qui. Minim eu ea deserunt reprehenderit tempor culpa elit amet ullamco culpa sit magna ut.
',NULL,'SHEREE','AQAAAAEAACcQAAAAEKHP4A1dm+hXEUW3RO3HxmW+DTfb86yOSlvfTj/DaJLOkd5nIsZ4NZ9HWazHmXQmEQ==',NULL,0,'S6RDZ2XYJZK2LHH6DI3R4N5C3VGRWKKG',0,'sheree');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
3,0,'Brule','08fcbc3c-edae-4e8c-950f-4721b8107f18','Niue','2021-07-12 00:00:00','1965-04-06 00:00:00',NULL,0,'female','Non nulla reprehenderit commodo magna anim ut laborum esse irure.','Occaecat ex amet culpa consectetur deserunt est proident elit sunt nisi sit esse reprehenderit. Magna esse aliquip incididunt esse ad ullamco pariatur adipisicing velit consectetur tempor. Nulla cillum laboris sunt velit Lorem incididunt. Mollit Lorem dolor fugiat Lorem sunt laborum pariatur adipisicing ex labore proident nostrud.
','Eliza','2021-10-30 19:29:39.309118',1,NULL,'Ullamco qui elit nulla ut nulla ut et eiusmod consectetur qui anim. Anim enim nisi sint occaecat minim ut nisi dolore ipsum officia laborum ullamco et. Lorem ut exercitation duis ipsum cillum adipisicing. Culpa consectetur exercitation dolore exercitation non magna est eu incididunt dolor adipisicing duis in anim.
',NULL,'ELIZA','AQAAAAEAACcQAAAAEKqYtKAnINT0AmUbZ9ypSiWQlm2BX15UJrt+dndpLPHoO1Bn6PKOIABf0tuKEvD+HA==',NULL,0,'YR3P3EB3TT6XBOJO7KREDG2V656O5BIG',0,'eliza');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
4,0,'Tilden','db63a059-f690-4fd5-85ce-7b442ffb58f0','Norway','2021-08-02 00:00:00','1952-10-11 00:00:00',NULL,0,'female','Consectetur laborum proident incididunt officia commodo ad voluptate in quis aliquip ad exercitation.','Voluptate reprehenderit et magna officia ea duis. Velit aute excepteur velit consectetur. Officia labore quis adipisicing deserunt labore adipisicing irure culpa Lorem.
','Terrie','2021-04-23 00:00:00',1,NULL,'Voluptate consectetur ea eu quis quis consectetur et occaecat anim officia consectetur nulla in. In anim in eu cillum Lorem proident id tempor minim dolore eu voluptate nostrud. Esse aliqua occaecat proident non deserunt proident veniam sunt enim tempor dolor. Proident qui ea pariatur ipsum. Anim nisi exercitation Lorem eiusmod. Elit nulla qui sit id. Ex tempor ullamco incididunt ut quis qui aliqua culpa.
',NULL,'TERRIE','AQAAAAEAACcQAAAAEHidRL7I9hIn1ndNymy75+FqFPViQN3d6QmtjOUx24VbGziUq7c68XarG8vL28rPRA==',NULL,0,'4NVJT7KFCTR4AEFWXAYV5THTJNWY4DN7',0,'terrie');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
5,0,'Savannah','686cc57e-f2b2-4b64-b172-44c38c02f41c','Syria','2021-03-01 00:00:00','1970-11-16 00:00:00',NULL,0,'female','Nulla tempor id in veniam eu.','Cillum aliquip consequat aute id non reprehenderit in dolor ea. Anim eu tempor deserunt qui nisi. Exercitation sit nulla enim labore aute reprehenderit ut aliqua ad. In est dolor enim eiusmod minim aliqua nisi do. Sit laboris voluptate non fugiat sint commodo ex tempor anim non labore.
','Bridget','2021-07-29 00:00:00',1,NULL,'Tempor sint laborum nostrud excepteur qui excepteur eiusmod dolor velit eiusmod est elit. Laborum sit veniam enim aliquip. Lorem aute veniam voluptate proident.
',NULL,'BRIDGET','AQAAAAEAACcQAAAAENYf5IcXRX0qf1rN1kxwORurjwRNkqy7+Thm+er57sGrHW8+ZLYkOORfib84ayRQsQ==',NULL,0,'WLN2V2QQHNPKZEK3PCRZM3CMQWA4GBQC',0,'bridget');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
6,0,'Gilgo','d2d210c3-8bd4-49d0-8f7f-f9fa9280b1f0','Saint Lucia','2021-07-15 00:00:00','1990-01-15 00:00:00',NULL,0,'male','Eiusmod eiusmod aliquip ullamco laborum commodo sint Lorem exercitation.','Est labore quis esse labore. Aliqua dolor proident ex do duis. Dolor amet laboris cillum Lorem irure velit. Commodo qui ex sunt reprehenderit anim ad. Et incididunt qui incididunt aliqua enim voluptate minim reprehenderit nulla. Sunt officia amet magna incididunt qui ex fugiat ea eiusmod elit proident pariatur Lorem duis.
','Graves','2021-04-21 00:00:00',1,NULL,'Esse nostrud commodo mollit eu irure Lorem in. Sint magna ad labore labore deserunt eiusmod deserunt id cupidatat. Magna duis ex sit cillum nisi irure eiusmod nisi esse anim exercitation officia esse et. Quis elit id nostrud nostrud magna sint qui ea elit in pariatur tempor. Est ipsum dolor do non occaecat in aliquip esse pariatur commodo consequat. Eu consectetur commodo in non adipisicing excepteur labore nisi proident culpa adipisicing. Sit est laborum adipisicing aliqua tempor commodo sunt minim anim incididunt deserunt.
',NULL,'GRAVES','AQAAAAEAACcQAAAAEN5n5AlMPEMJQUbjNl+ziArM+gLwVKzxjgsu9wANzQP4SbN2+WPOvMG6KG2k7hVy9Q==',NULL,0,'TJ5VHFVWFFBLKAT3PTNH6PYSJGPWXGGX',0,'graves');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
7,0,'Hilltop','f4e07ead-3719-4292-9cce-18ed232f2c9d','Seychelles','2021-06-21 00:00:00','1988-11-29 00:00:00',NULL,0,'male','Labore consequat dolore mollit tempor deserunt ad cupidatat magna aliqua est laboris.','Qui laborum cupidatat fugiat consectetur commodo proident laboris anim eiusmod qui labore in ad cillum. Mollit tempor est veniam excepteur ullamco ut nulla adipisicing deserunt. Fugiat nisi excepteur cillum minim esse duis labore voluptate consectetur magna ut culpa. Adipisicing occaecat ipsum deserunt qui excepteur aute ullamco nisi.
','Mccray','2020-05-05 00:00:00',1,NULL,'Non ex fugiat esse eiusmod consequat ut culpa nostrud. Et consectetur amet commodo elit eu labore exercitation ipsum sint. Magna et cillum cillum proident sit eiusmod nulla anim laboris elit sint.
',NULL,'MCCRAY','AQAAAAEAACcQAAAAEJMZOGDoFONrps82EO17YPEFiguixQ3K79EHTDM0FO9ZHAFl/ltwUYbSGxZRFkCSSA==',NULL,0,'V2Y5QH7LL42Z7JVMI6E2F6BP73L5CEGF',0,'mccray');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
8,0,'Rote','c2a0a51b-18ab-4aa7-8bc7-5bd6dede85bd','Chile','2021-04-01 00:00:00','1984-12-26 00:00:00',NULL,0,'male','Tempor tempor ipsum dolor mollit proident.','Velit dolor commodo non id ullamco proident magna velit. Non magna qui laboris ut labore laborum velit. Adipisicing consectetur excepteur esse cupidatat adipisicing qui laboris cupidatat voluptate nisi mollit enim nisi.
','WarnerKinngg','2021-10-30 23:33:35.09993',1,NULL,'Irure commodo eiusmod tempor nisi ut ut nostrud. Esse nostrud minim cillum veniam aliqua amet aliqua non in tempor excepteur cupidatat dolor. Laborum incididunt enim mollit velit ipsum elit. Et exercitation aliqua anim sit ullamco eiusmod magna quis reprehenderit deserunt exercitation. Laborum officia quis do excepteur ex exercitation. Aute dolore et officia dolor enim aute ad laboris deserunt. Magna veniam Lorem laboris minim sit sint ad enim nisi amet.
',NULL,'WARNER','AQAAAAEAACcQAAAAEIaApnLzVhZlnLflye4QVgiOWlKxfhDnG9H/PmEOIIPsTEZRyaSzm7QHT7GzHtqntw==',NULL,0,'HYNMVT2WKGOKH3DBBRCBXPG7TUSJHMBA',0,'warner');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
9,0,'Elizaville','a9e5e364-9e92-4a2e-96d7-8a945a531c38','Belarus','2021-03-21 00:00:00','1975-02-26 00:00:00',NULL,0,'male','Id sint nostrud incididunt non tempor minim aute.','Consectetur ullamco labore minim laborum sint dolor consectetur duis labore fugiat. Quis aliqua fugiat nisi magna dolor dolor. In ea proident mollit ipsum adipisicing consequat voluptate adipisicing pariatur nisi est dolore proident. Adipisicing occaecat cillum consectetur nisi laboris duis.
','Dixon','2021-07-24 00:00:00',1,NULL,'Officia Lorem dolore ut aliquip eu dolore voluptate magna irure reprehenderit esse enim sint. Nostrud sint eiusmod quis ad incididunt nisi Lorem consequat anim sit fugiat dolore aliquip elit. Anim cillum labore duis aliquip. Cupidatat ut cupidatat eu voluptate voluptate nisi minim magna proident adipisicing consectetur. Commodo amet sunt reprehenderit tempor consectetur dolore tempor laborum nostrud do deserunt. Consectetur elit ex dolore id magna minim nostrud est duis nostrud Lorem. Exercitation do magna esse reprehenderit nisi deserunt laboris est.
',NULL,'DIXON','AQAAAAEAACcQAAAAEHvvdnhy7fJUPTDWRE2Vk38odfd+JR9nNqNJcfS0hac0LMFxwcBG9SCxBje8fgY6bw==',NULL,0,'ULCUT5KQD7KGVGI7R4SHTM3VBJFZ6OWF',0,'dixon');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
10,0,'Kipp','aed894ba-a284-4c6d-8ab3-a351ac02900e','Nepal','2021-01-10 00:00:00','1968-02-25 00:00:00',NULL,0,'male','Lorem elit irure ex amet labore laborum mollit pariatur ipsum.','Exercitation dolore aliqua sunt consectetur officia incididunt sit. Id nostrud irure pariatur sit tempor. Consequat exercitation laboris eu qui ea anim proident nostrud enim in excepteur velit.
','Woodward','2021-07-17 00:00:00',1,NULL,'Anim ad cupidatat amet mollit id laborum qui aliquip. Aliquip esse esse nostrud aliqua veniam. Cupidatat ea deserunt consectetur exercitation quis sit quis ut exercitation. Adipisicing commodo minim dolore sunt ut sunt Lorem officia elit. Et duis sit occaecat magna tempor enim amet tempor laboris. Cupidatat ea officia ullamco laborum dolore magna ut labore et exercitation Lorem laboris. Ullamco veniam qui ex et.
',NULL,'WOODWARD','AQAAAAEAACcQAAAAELloWCegv/lFyuw+Fv0TPM4KD5aScboUXnJvrR696d/zMf+ucryP0nXxKBVkDNPXHg==',NULL,0,'7F43TZZFY4YQCVJUZRB73QPXMATEK7AO',0,'woodward');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
11,0,NULL,'fd4cccd7-5865-4ad1-8f85-4799eed560a3',NULL,'2021-10-28 12:55:00.3254872','0001-01-01 00:00:00',NULL,0,NULL,NULL,NULL,NULL,'2021-12-18 19:19:44.8268689',1,NULL,NULL,NULL,'ADMIN','AQAAAAEAACcQAAAAELeFc2C0t5TXr6NtxwtiC+DJkSybXyvqd8sxEPH8orbLp74WPkuZ4qHoD0yF4/S+yA==',NULL,0,'XRWUHSWBNYR53GQXJCLNSOWYYZ55S3XL',0,'admin');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
12,0,'Prishtina','b38bed36-c6f1-4eee-a7c5-81503efceeea','Kosova','2021-10-30 14:32:10.3353073','2003-10-06 12:31:26',NULL,0,'male','hei','Hei','gentrit','2021-10-30 19:31:52.282948',1,NULL,'hei',NULL,'GENTI','AQAAAAEAACcQAAAAEGw19/ecmav5qacKymKh0rzc2iuBUBnJ2mTWuMjVImIa2lklB9TYlFQfzuD5dG/k9g==',NULL,0,'3GR3TDCBJWZSV6BTC3LO4YMNODURBCE6',0,'genti');
INSERT INTO [AspNetUsers] ([Id],[AccessFailedCount],[City],[ConcurrencyStamp],[Country],[Created],[DateOfBirth],[Email],[EmailConfirmed],[Gender],[Interests],[Introduction],[KnownAs],[LastActive],[LockoutEnabled],[LockoutEnd],[LookingFor],[NormalizedEmail],[NormalizedUserName],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[SecurityStamp],[TwoFactorEnabled],[UserName]) VALUES (
13,0,'pri','693858af-c1c9-4fb9-9013-6c641522a4ba','pr','2021-12-18 19:15:40.5416233','2003-12-18 18:11:53',NULL,0,'male',NULL,NULL,'gg','2021-12-18 19:18:51.7883084',1,NULL,NULL,NULL,'GG','AQAAAAEAACcQAAAAEIXboAu4riUqZCGz7Ql1jSjmNjV5c5vw/flbhFd1MwY7+B7LkzAee1eqnuhBZ4yaig==',NULL,0,'GADTOQOLUBTICERS3TNWQ5CAD5ACH3GS',0,'gg');
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
1,1,1,NULL,'https://randomuser.me/api/portraits/women/33.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
2,2,1,NULL,'https://randomuser.me/api/portraits/women/18.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
3,3,1,NULL,'https://randomuser.me/api/portraits/women/79.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
4,4,1,NULL,'https://randomuser.me/api/portraits/women/99.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
5,5,1,NULL,'https://randomuser.me/api/portraits/women/44.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
6,6,1,NULL,'https://randomuser.me/api/portraits/men/23.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
7,7,1,NULL,'https://randomuser.me/api/portraits/men/11.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
8,8,1,NULL,'https://randomuser.me/api/portraits/men/15.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
9,9,1,NULL,'https://randomuser.me/api/portraits/men/24.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
10,10,1,NULL,'https://randomuser.me/api/portraits/men/70.jpg',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
11,3,0,'qs012z4aalrbbdtwcpow','https://res.cloudinary.com/dxzlmp9tb/image/upload/v1635420692/qs012z4aalrbbdtwcpow.png',1);
INSERT INTO [Photos] ([Id],[AppUserId],[IsMain],[PublicId],[Url],[IsApproved]) VALUES (
14,13,1,'yvk1gkdouewayfzfdiv6','https://res.cloudinary.com/dxzlmp9tb/image/upload/v1639851523/yvk1gkdouewayfzfdiv6.jpg',1);
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
1,'sss',NULL,'2021-10-28 18:51:12.5151973',0,1,'sherri',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
2,'heii','2021-10-29 15:16:21.8097853','2021-10-28 19:29:27.6988189',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
3,'ss','2021-10-29 15:15:36.4220896','2021-10-28 23:19:33.2966426',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
4,'sss',NULL,'2021-10-28 23:30:28.744032',0,1,'sherri',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
5,'sss',NULL,'2021-10-28 23:34:42.402362',0,3,'eliza',0,11,'admin');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
6,'hello liza','2021-10-29 15:36:27.6261672','2021-10-29 15:36:21.8059187',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
7,'hey','2021-10-29 15:36:50.5840119','2021-10-29 15:36:29.5826837',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
8,'sss','2021-10-29 15:37:50.9000289','2021-10-29 15:36:57.4910743',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
9,'hei','2021-10-29 15:37:50.9000347','2021-10-29 15:37:44.2433696',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
10,'ss','2021-10-29 15:41:00.1721082','2021-10-29 15:40:56.663934',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
11,'ss','2021-10-29 15:41:31.5175231','2021-10-29 15:41:10.8205108',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
12,'hei','2021-10-29 15:41:28.7021093','2021-10-29 15:41:21.3513534',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
13,'hello warner','2021-10-29 15:42:33.976562','2021-10-29 15:42:25.2552994',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
14,'hei liza','2021-10-29 15:47:41.7029149','2021-10-29 15:46:58.0847149',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
15,'xx','2021-10-29 15:47:41.7029225','2021-10-29 15:47:17.4921537',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
16,'ss','2021-10-29 15:47:41.7029232','2021-10-29 15:47:26.3372639',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
17,'ss','2021-10-29 15:48:53.6418349','2021-10-29 15:48:06.2182548',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
18,'ddddddddddddd','2021-10-29 15:48:53.6418409','2021-10-29 15:48:09.0731414',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
19,'r','2021-10-29 15:48:55.6843275','2021-10-29 15:48:12.197648',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
20,'ttttttttt','2021-10-29 16:15:37.054811','2021-10-29 15:48:59.6199646',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
21,'hey warner','2021-10-29 16:15:53.3405799','2021-10-29 16:15:46.770465',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
22,'s','2021-10-29 16:22:24.0325746','2021-10-29 16:19:28.2518658',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
23,'hey','2021-10-29 16:28:26.1975972','2021-10-29 16:28:03.666299',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
24,'uu','2021-10-29 16:40:41.2113726','2021-10-29 16:34:15.9339302',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
25,'sss','2021-10-29 16:40:41.2115762','2021-10-29 16:35:03.0805075',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
26,'hh','2021-10-29 18:58:25.3195963','2021-10-29 16:40:49.7074761',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
27,'aaaa','2021-10-29 18:58:25.3196021','2021-10-29 16:43:33.7625392',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
28,'aaaaaa','2021-10-29 18:58:25.3196028','2021-10-29 16:45:28.6460854',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
29,'aaa','2021-10-29 18:58:25.3196035','2021-10-29 16:49:04.0434151',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
30,'s','2021-10-29 18:58:25.3196041','2021-10-29 16:50:50.6279997',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
31,'hey','2021-10-29 17:23:07.4838929','2021-10-29 17:23:03.1830707',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
32,'eyy','2021-10-29 17:24:02.915555','2021-10-29 17:23:36.6261853',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
33,'eee','2021-10-29 17:24:02.9155572','2021-10-29 17:23:47.6263708',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
34,'eee','2021-10-29 17:24:02.9155574','2021-10-29 17:23:59.8854954',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
35,'eee','2021-10-29 17:25:19.1366139','2021-10-29 17:25:14.3320657',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
36,'hei','2021-10-29 19:28:51.3927936','2021-10-29 19:28:34.7978414',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
37,'e','2021-10-29 19:28:51.3930196','2021-10-29 19:28:48.6026107',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
38,'sss','2021-10-29 19:30:57.6770892','2021-10-29 19:30:54.1408926',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
39,'hei eliza','2021-10-29 22:46:37.8761213','2021-10-29 22:46:33.9792835',0,3,'eliza',0,8,'warner');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
40,'assa','2021-10-29 22:46:56.2861968','2021-10-29 22:46:41.4371408',0,8,'warner',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
41,'hei','2021-10-30 14:32:56.2434941','2021-10-30 14:32:52.4268165',0,3,'eliza',0,12,'genti');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
42,'hei gent','2021-10-30 14:33:16.0156806','2021-10-30 14:33:12.1682253',1,12,'genti',0,3,'eliza');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
43,'hei liza','2021-10-30 18:45:48.32776','2021-10-30 18:45:45.0702944',0,3,'eliza',0,12,'genti');
INSERT INTO [Messages] ([Id],[Content],[DateRead],[MessageSent],[RecipientDeleted],[RecipientId],[RecipientUsername],[SenderDeleted],[SenderId],[SenderUsername]) VALUES (
44,'Heyy',NULL,'2021-12-18 19:16:19.6168729',0,3,'eliza',0,13,'gg');
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
3,8);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,1);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,2);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,3);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,4);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,5);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,6);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
8,10);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
12,3);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
13,3);
INSERT INTO [Likes] ([SourceUserId],[LikeUserId]) VALUES (
13,4);
INSERT INTO [AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) VALUES (
1,'Member','MEMBER','eccac951-84fe-4739-8646-ff18927e622f');
INSERT INTO [AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) VALUES (
2,'Admin','ADMIN','c243cab1-0419-4502-a666-a93702fd7766');
INSERT INTO [AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp]) VALUES (
3,'Moderator','MODERATOR','f51f3a10-cf9f-41ad-904e-8b73093ac0e9');
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
1,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
2,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
3,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
4,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
5,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
5,3);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
6,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
7,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
8,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
9,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
10,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
11,2);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
11,3);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
12,1);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
12,2);
INSERT INTO [AspNetUserRoles] ([UserId],[RoleId]) VALUES (
13,1);
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20210614113725_InitialCreate','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20210616112456_UserPasswordAdded','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20210705133846_ExtendedUserClass','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20210705182934_UpdateProperty','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20211025101136_LikeEntityAdded','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20211025183443_MessageEntityAdded','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20211027102332_IdentityAdded','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20211028105152_PhotoApprovalAdded','5.0.11');
INSERT INTO [__EFMigrationsHistory] ([MigrationId],[ProductVersion]) VALUES (
'20211029140619_GroupsAdded','5.0.11');
CREATE INDEX [Connections_IX_Connections_GroupName] ON [Connections] ([GroupName] ASC);
CREATE UNIQUE INDEX [AspNetUsers_UserNameIndex] ON [AspNetUsers] ([NormalizedUserName] ASC);
CREATE INDEX [AspNetUsers_EmailIndex] ON [AspNetUsers] ([NormalizedEmail] ASC);
CREATE INDEX [Photos_IX_Photos_AppUserId] ON [Photos] ([AppUserId] ASC);
CREATE INDEX [Messages_IX_Messages_SenderId] ON [Messages] ([SenderId] ASC);
CREATE INDEX [Messages_IX_Messages_RecipientId] ON [Messages] ([RecipientId] ASC);
CREATE INDEX [Likes_IX_Likes_LikeUserId] ON [Likes] ([LikeUserId] ASC);
CREATE INDEX [AspNetUserLogins_IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId] ASC);
CREATE INDEX [AspNetUserClaims_IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId] ASC);
CREATE UNIQUE INDEX [AspNetRoles_RoleNameIndex] ON [AspNetRoles] ([NormalizedName] ASC);
CREATE INDEX [AspNetUserRoles_IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId] ASC);
CREATE INDEX [AspNetRoleClaims_IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId] ASC);
CREATE TRIGGER [fki_Connections_GroupName_Groups_Name] BEFORE Insert ON [Connections] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Connections violates foreign key constraint FK_Connections_0_0') WHERE NEW.GroupName IS NOT NULL AND(SELECT Name FROM Groups WHERE  Name = NEW.GroupName) IS NULL; END;
CREATE TRIGGER [fku_Connections_GroupName_Groups_Name] BEFORE Update ON [Connections] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Connections violates foreign key constraint FK_Connections_0_0') WHERE NEW.GroupName IS NOT NULL AND(SELECT Name FROM Groups WHERE  Name = NEW.GroupName) IS NULL; END;
CREATE TRIGGER [fki_Photos_AppUserId_AspNetUsers_Id] BEFORE Insert ON [Photos] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Photos violates foreign key constraint FK_Photos_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.AppUserId) IS NULL; END;
CREATE TRIGGER [fku_Photos_AppUserId_AspNetUsers_Id] BEFORE Update ON [Photos] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Photos violates foreign key constraint FK_Photos_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.AppUserId) IS NULL; END;
CREATE TRIGGER [fki_Messages_SenderId_AspNetUsers_Id] BEFORE Insert ON [Messages] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Messages violates foreign key constraint FK_Messages_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.SenderId) IS NULL; END;
CREATE TRIGGER [fku_Messages_SenderId_AspNetUsers_Id] BEFORE Update ON [Messages] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Messages violates foreign key constraint FK_Messages_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.SenderId) IS NULL; END;
CREATE TRIGGER [fki_Messages_RecipientId_AspNetUsers_Id] BEFORE Insert ON [Messages] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Messages violates foreign key constraint FK_Messages_1_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.RecipientId) IS NULL; END;
CREATE TRIGGER [fku_Messages_RecipientId_AspNetUsers_Id] BEFORE Update ON [Messages] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Messages violates foreign key constraint FK_Messages_1_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.RecipientId) IS NULL; END;
CREATE TRIGGER [fki_Likes_SourceUserId_AspNetUsers_Id] BEFORE Insert ON [Likes] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Likes violates foreign key constraint FK_Likes_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.SourceUserId) IS NULL; END;
CREATE TRIGGER [fku_Likes_SourceUserId_AspNetUsers_Id] BEFORE Update ON [Likes] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Likes violates foreign key constraint FK_Likes_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.SourceUserId) IS NULL; END;
CREATE TRIGGER [fki_Likes_LikeUserId_AspNetUsers_Id] BEFORE Insert ON [Likes] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Likes violates foreign key constraint FK_Likes_1_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.LikeUserId) IS NULL; END;
CREATE TRIGGER [fku_Likes_LikeUserId_AspNetUsers_Id] BEFORE Update ON [Likes] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Likes violates foreign key constraint FK_Likes_1_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.LikeUserId) IS NULL; END;
CREATE TRIGGER [fki_AspNetUserTokens_UserId_AspNetUsers_Id] BEFORE Insert ON [AspNetUserTokens] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetUserTokens violates foreign key constraint FK_AspNetUserTokens_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fku_AspNetUserTokens_UserId_AspNetUsers_Id] BEFORE Update ON [AspNetUserTokens] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetUserTokens violates foreign key constraint FK_AspNetUserTokens_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fki_AspNetUserLogins_UserId_AspNetUsers_Id] BEFORE Insert ON [AspNetUserLogins] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetUserLogins violates foreign key constraint FK_AspNetUserLogins_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fku_AspNetUserLogins_UserId_AspNetUsers_Id] BEFORE Update ON [AspNetUserLogins] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetUserLogins violates foreign key constraint FK_AspNetUserLogins_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fki_AspNetUserClaims_UserId_AspNetUsers_Id] BEFORE Insert ON [AspNetUserClaims] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetUserClaims violates foreign key constraint FK_AspNetUserClaims_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fku_AspNetUserClaims_UserId_AspNetUsers_Id] BEFORE Update ON [AspNetUserClaims] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetUserClaims violates foreign key constraint FK_AspNetUserClaims_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fki_AspNetUserRoles_UserId_AspNetUsers_Id] BEFORE Insert ON [AspNetUserRoles] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetUserRoles violates foreign key constraint FK_AspNetUserRoles_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fku_AspNetUserRoles_UserId_AspNetUsers_Id] BEFORE Update ON [AspNetUserRoles] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetUserRoles violates foreign key constraint FK_AspNetUserRoles_0_0') WHERE (SELECT Id FROM AspNetUsers WHERE  Id = NEW.UserId) IS NULL; END;
CREATE TRIGGER [fki_AspNetUserRoles_RoleId_AspNetRoles_Id] BEFORE Insert ON [AspNetUserRoles] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetUserRoles violates foreign key constraint FK_AspNetUserRoles_1_0') WHERE (SELECT Id FROM AspNetRoles WHERE  Id = NEW.RoleId) IS NULL; END;
CREATE TRIGGER [fku_AspNetUserRoles_RoleId_AspNetRoles_Id] BEFORE Update ON [AspNetUserRoles] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetUserRoles violates foreign key constraint FK_AspNetUserRoles_1_0') WHERE (SELECT Id FROM AspNetRoles WHERE  Id = NEW.RoleId) IS NULL; END;
CREATE TRIGGER [fki_AspNetRoleClaims_RoleId_AspNetRoles_Id] BEFORE Insert ON [AspNetRoleClaims] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table AspNetRoleClaims violates foreign key constraint FK_AspNetRoleClaims_0_0') WHERE (SELECT Id FROM AspNetRoles WHERE  Id = NEW.RoleId) IS NULL; END;
CREATE TRIGGER [fku_AspNetRoleClaims_RoleId_AspNetRoles_Id] BEFORE Update ON [AspNetRoleClaims] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table AspNetRoleClaims violates foreign key constraint FK_AspNetRoleClaims_0_0') WHERE (SELECT Id FROM AspNetRoles WHERE  Id = NEW.RoleId) IS NULL; END;
COMMIT;

