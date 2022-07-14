INSERT INTO "Donations"
VALUES (:ChannelId, :DonatorName, :Amount)
ON CONFLICT ("ChannelId", "DonatorName")
    DO UPDATE SET "Amount" = :Amount