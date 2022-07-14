DELETE FROM "Donations"
WHERE "ChannelId" = :ChannelId;

DELETE FROM "ChannelSettings"
WHERE "ChannelId" = :ChannelId;