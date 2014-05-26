CREATE TABLE [dbo].[OrderActivity]
(
	[OrderId] INT NOT NULL,
	[ActivityId] INT NOT NULL, 

    CONSTRAINT [PK_OrderActivity] PRIMARY KEY ([OrderId], [ActivityId]), 

    CONSTRAINT [FK_OrderActivity_ToOrder] FOREIGN KEY ([OrderId]) REFERENCES [Order]([Id]),
    CONSTRAINT [FK_OrderActivity_ToEvent] FOREIGN KEY ([ActivityId]) REFERENCES [Activity]([Id])
)
