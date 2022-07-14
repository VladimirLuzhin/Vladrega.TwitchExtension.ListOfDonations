SELECT "DonatorName", "Amount" 
FROM "Donations"
WHERE "ChannelId" = :ChannelId
ORDER BY "Amount" DESC