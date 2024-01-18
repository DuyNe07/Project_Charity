USE Charity


GO

CREATE TRIGGER [TRG_CheckDonor]
ON Donor
FOR INSERT, UPDATE
AS
BEGIN
	DEClARE @NgaySinh DATE;
	DECLARE @Tuoi INT;

	SELECT @NgaySinh = Birthday
	FROM inserted;

	SET @Tuoi = DATEDIFF(YEAR, @NgaySinh, GETDATE());
	IF @Tuoi < 18
	BEGIN
		RAISERROR('Người dùng không đủ 18 tuổi', 16, 1);
		ROLLBACK TRANSACTION;
	END
END;

GO

CREATE TRIGGER [TRG_CheckCharities]
ON Charities
FOR INSERT, UPDATE
AS
BEGIN
	DEClARE @NgaySinh DATE;
	DECLARE @Tuoi INT;

	SELECT @NgaySinh = Birthday
	FROM inserted;

	SET @Tuoi = DATEDIFF(YEAR, @NgaySinh, GETDATE());
	IF @Tuoi < 18
	BEGIN
		RAISERROR('Người dùng không đủ 18 tuổi', 16, 1);
		ROLLBACK TRANSACTION;
	END
END;

GO
DROP TRIGGER [TRG_CheckCampaign]
CREATE TRIGGER [TRG_CheckCampaign]
ON Campaign
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @Begin DATE, @End DATE;

	SELECT @Begin = DateBegin, @End = DateEnd
	FROM inserted;

	IF @End < @Begin
	BEGIN
		RAISERROR('Ngày kết thúc chiến dịch phải lớn hơn ngày bắt đầu', 16, 1);
		ROLLBACK TRANSACTION;
	END
END

GO