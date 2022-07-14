INSERT INTO "ChannelSettings"
VALUES (:ChannelId, :Theme)
ON CONFLICT ("ChannelId")
    DO UPDATE SET "Theme" = :Theme;