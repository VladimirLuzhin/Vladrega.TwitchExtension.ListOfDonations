import React, { useRef, useState, useCallback, useMemo } from 'react'
import { LocalizationResources, LocalizationType } from '../../resources/Localization'

import RuFlag from '../../resources/ru-flag.png'
import EngFlag from '../../resources/en-flag.png'
import { Button } from '../Button/Button'



export const ConfigPresenter = ({ onDeleteAsync, onSaveAsync }) => {
    const [selectedLocalization, selectLocalization] = useState(LocalizationType.RU)
    const [errorText, setErorrText] = useState(null)

    const textAreaRef = useRef()
    const themeRef = useRef()

    const onAction = useCallback(async (action) => {
        const result = await action()
        if (result)
            return result

        setErorrText(currentLocale.configErrorMessage)
        setTimeout(() => {
            setErorrText(null)
        }, 10_000)

        return result
    }, )

    const onSaveInternalAsync = useCallback(async () => {
        await onAction(async () => await onSaveAsync(parseInt(themeRef.current.value), textAreaRef.current.value))
    }, [onSaveAsync, themeRef, textAreaRef])

    const onDeletInternalAsync = useCallback(async () => {
        await onAction(async () => await onDeleteAsync())
    }, [onDeleteAsync])

    const currentLocale = useMemo(() => LocalizationResources[selectedLocalization], [selectedLocalization])

    return <div className='step'>
        <div className='localizations'>
            <img src={RuFlag} className={`localization ${selectedLocalization === LocalizationType.RU ? 'selected' : ''}`} onClick={() => selectLocalization(LocalizationType.RU)}/>
            <img src={EngFlag} className={`localization ${selectedLocalization === LocalizationType.EN ? 'selected' : ''}`} onClick={() => selectLocalization(LocalizationType.EN)}/>            
        </div>
        <div className='instruction'>
            {currentLocale.instractionAboutDonationAlertsLink}
        </div>
        <div className='example'>
            {currentLocale.example}
        </div>
        <div className='theme'>
            <label>{currentLocale.labelForTheme}</label>
            <select title={currentLocale.themeSelectTitle} ref={themeRef}>
                <option value={0}>-</option>
                <option value={1}>Heores of Might and Magic III</option>
                <option value={2}>Disciples 2</option>
            </select>
        </div>
        <div className='put-link-row'>
            <textarea ref={textAreaRef} rows={12} />
        </div>
        <div className='buttons'>
            <Button title={currentLocale.saveButtonTitle} name={currentLocale.saveButtonText}  onClick={onSaveInternalAsync}/>
            <Button title={currentLocale.deleteButtonTitle} name={currentLocale.deleteButtonText} onClick={onDeletInternalAsync}/>
        </div>
        <div className='error'>
            {errorText}
        </div>
    </div>
}