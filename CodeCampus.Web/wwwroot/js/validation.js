document.addEventListener('DOMContentLoaded', function () {


    const formErrorHandler = (element, validationResult) => {
        let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

        if (validationResult) {
            element.classList.remove('input-validation-error')
            spanElement.classList.remove('field-validation-error')
            spanElement.classList.add('field-validation-valid')
            spanElement.innerHTML = ''
        }
        else {
            element.classList.add('input-validation-error')
            spanElement.classList.add('field-validation-error',)
            spanElement.classList.remove('field-validation-valid')
            spanElement.innerHTML = element.dataset.valRequired
        }
    }

    const textValidator = (element, minLength = 2) => {

        if (element.name === "AddressInfo.Addressline_2") {
            return;
        }

        if (element.value.length >= minLength) {
            formErrorHandler(element, true)
        }
        else {
            formErrorHandler(element, false)
        }

    }

    const emailValidator = (element) => {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        formErrorHandler(element, regex.test(element.value))
    }

    const passwordValidator = (element) => {
        if (element.dataset.valEqualtoOther !== undefined) {

            let password = document.getElementsByName(element.dataset.valEqualtoOther.replace('*', 'Form'))[0].value

            if (element.value === password)
                formErrorHandler(element, true)
            else
                formErrorHandler(element, false)
        }
        else {
            const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
            formErrorHandler(element, regex.test(element.value))
        }
    }

    const postalcodeValidator = (element) => {
        const regex = /^[0-9a-zA-Z\s]{3,10}$/
        formErrorHandler(element, regex.test(element.value))
    }

    const checkboxValidator = (element) => {
        if (element.checked) {
            formErrorHandler(element, true)
        }
        else {
            formErrorHandler(element, false)

        }
    }

    let forms = document.querySelectorAll('form')

    forms.forEach(form => {
        let inputs = form.querySelectorAll('input')

        inputs.forEach(input => {
            if (input.dataset.val === 'true') {

                if (input.type === 'checkbox') {
                    input.addEventListener('change', (e) => {
                        checkboxValidator(e.target)
                    })
                }
                else {
                    input.addEventListener('keyup', (e) => {

                        switch (e.target.type) {

                            case 'text':
                                textValidator(e.target)
                                break;

                            case 'email':
                                emailValidator(e.target)
                                break;

                            case 'password':
                                passwordValidator(e.target)
                                break;

                            case 'postalcode':
                                postalcodeValidator(e.target)
                                break;
                         
                        }
                    })
                }
            }
        })
    })

    const cancelSecurityButton = document.getElementById('cancelSecurity');

    if (cancelSecurityButton) {
        cancelSecurityButton.addEventListener('click', function () {
            resetForm('#securityForm');
        });
    } else {
        console.error("Cancel button not found");
    }



    document.getElementById('cancelBasicDetails').addEventListener('click', function() {
        resetForm('#basicDetailsForm')
    })
    document.getElementById('cancelAddressDetails').addEventListener('click', function() {
        resetForm('#addressDetailsForm')
    })

    function resetForm(formSelector) {
        const form = document.querySelector(formSelector)
        form.reset()

        const errorElements = form.querySelectorAll('.input-validation-error, .field-validation-error, .field-validation-valid');
        errorElements.forEach(element => {
            element.classList.remove('input-validation-error', 'field-validation-error', 'field-validation-valid');
            if (element.tagName.toLowerCase() === 'span') {
                element.textContent = '';
            }
        });
    }
})

