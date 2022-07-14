 class DonationRepository {
    backendUrl = 'https://vladre.ga'

    async saveDonationsAsync(channelId, theme, donations) {
        const response = await fetch(`${this.backendUrl}/api/v1/donations`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ channelId, theme, donations })
        })

        if (!response.ok)
            throw new Error(`Can't save donations, reason: ${await response.text()}`)
    }

    async deleteAllInformationAsync(channelId) {
        const response = await fetch(`${this.backendUrl}/api/v1/donations?channelId=${channelId}`, {
            method: 'DELETE'
        })

        if (!response.ok)
            throw new Error(`Can't delete channel data, reason: ${await response.text()}`)
    }

    async getDonationsListAsync(channelId) {
        const response = await fetch(`${this.backendUrl}/api/v1/donations?channelId=${channelId}`)

        if (!response.ok)
            throw new Error(`Can't get list of top donators, reason: ${await response.text()}`)

        return await response.json()
    }
 }

 export const donationRepository = new DonationRepository()