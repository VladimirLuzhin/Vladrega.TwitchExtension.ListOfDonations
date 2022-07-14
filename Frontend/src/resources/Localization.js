import ReactHtmlParser  from 'html-react-parser'

export const LocalizationType = {
    RU: 'RU',
    EN: 'EN'
}

export const LocalizationResources = {
    RU: {
        instractionAboutDonationAlertsLink: ReactHtmlParser("<div>Пожалуйста, разместите список ваших донатеров в текстовом поле ниже в формате:</br></br>{Имя донатера} - {Сумма донатов} или</br>{Имя донатера} {Сумма донатов}</br></br> и нажмите кнопку <u>Сохранить</u></br></br>Пример списка описан ниже:</br></div>"),
        saveButtonText: "Сохранить",
        deleteButtonText: "Удалить",
        linkInputPlaceholder: "Разместите ссылку здесь",
        configErrorMessage: "Что-то пошло не так, пожалуйста, попробуйте позже",
        headerOfTopDonators: "👑 Топ Донатеры",
        footerOfTopDonators: "Спасибо За Поддержку ❤️",
        errorOfTopDonators: ReactHtmlParser("Что-то Пошло Не Так, Попробуйте Перезагрузить Страницу<br/>💀"),
        labelForTheme: "Выберите тему для ТОП 3 донатеров",
        saveButtonTitle: "Сохранение списка новых донатеров",
        deleteButtonTitle: "Удаление всей информации о донатерах канала",
        themeSelectTitle: "У ТОП 3 донатеров будет тематическая аватарка",
        example: ReactHtmlParser("</br><code>Rusich_Ru - 12000р</br>voidptrt - 10000р</br>Geralt from Rivia - 10000 золотых</br>Gachibass - 300$</br>Batman - 100$</code><div>")
    },
    EN: {
        instractionAboutDonationAlertsLink: ReactHtmlParser("<div>Please place a list of your donators in the text field below in the format:</br></br>{Donator name} - {Donate amount} or</br>{Donator name} {Donate amount}</br></br > and click <u>Save</u></br></br>An example list is described below:</br></div>"),
        saveButtonText: "Save",
        deleteButtonText: "Delete",
        linkInputPlaceholder: "Put Link Here",
        configErrorMessage: "Something going wrong, please try again later",
        headerOfTopDonators: "👑 Top Donators",
        footerOfTopDonators: "Thank You For Support ❤️",
        errorOfTopDonators: ReactHtmlParser("Something Going Wrong, Please Try Reloading The Page <br/>💀"),
        labelForTheme: "Select a theme for the TOP 3 donators",
        saveButtonTitle: "Save a list of new donators",
        deleteButtonTitle: "Deleting all information about channel donnators",
        themeSelectTitle: "TOP 3 donatros will have a themed avatar",
        example: ReactHtmlParser("</br><code>Rusich_Ru - 12000р</br>voidptrt - 10000р</br>Geralt from Rivia - 10000 золотых</br>Gachibass - 300$</br>Batman - 100$</code><div>")
    }
}