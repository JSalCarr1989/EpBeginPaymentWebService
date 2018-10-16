CREATE TRIGGER [dbo].[T_INSERT_NEW_BEGINPAYMENT] ON [dbo].[BeginPayment]
  AFTER INSERT
AS 
BEGIN

  SET NOCOUNT ON 
  DECLARE @InsertedBeginPaymentId INT
  DECLARE @InsertedPaymentReference VARCHAR(50)
  DECLARE @InsertedServiceRequest VARCHAR(64)
  DECLARE @StatusDatetime DATETIME
  DECLARE @StatusPaymentListId INT 
  DECLARE @ResponsePaymentId INT = 0
  DECLARE @RequestPaymentId INT = 0
  DECLARE @EndPaymentId INT = 0
  
  SELECT @InsertedBeginPaymentId = [BeginPaymentId]
  FROM INSERTED 

  SELECT @InsertedPaymentReference = [PaymentReference]
  FROM INSERTED

  SELECT @InsertedServiceRequest = [ServiceRequest] 
  FROM INSERTED

  SET @StatusDatetime = GETDATE()

  SELECT @StatusPaymentListId = [IdStatusPaymentList]
  FROM StatusPaymentList WHERE [Description] = 'BEGIN_PAYMENT'
 
  





  INSERT INTO LogPayment(
                         PaymentReference,
						 ServiceRequest,
						 StatusDateTime,
						 StatusPaymentListId,
						 BeginPaymentId,
						 ResponsePaymentId,
						 RequestPaymentId,
						 EndPaymentId
						 )
				VALUES (
				        @InsertedPaymentReference,
						@InsertedServiceRequest,
						@StatusDatetime,
						@StatusPaymentListId,
						@InsertedBeginPaymentId,
						@ResponsePaymentId,
						@RequestPaymentId,
						@EndPaymentId
					   )

  SET NOCOUNT OFF
END 