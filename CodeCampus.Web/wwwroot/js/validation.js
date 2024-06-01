document.addEventListener('DOMContentLoaded', function () {

    const formErrorHandler = (element, validationResult, errorMessage = '') => {
        let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`)

        if (validationResult) {
            element.classList.remove('input-validation-error')
            element.classList.add('input-validation-success');
            spanElement.classList.remove('field-validation-error', 'hidden');
            spanElement.classList.add('field-validation-valid')
            spanElement.innerHTML = ''
        }
        else {
            element.classList.remove('input-validation-success');
            element.classList.add('input-validation-error')
            spanElement.classList.remove('field-validation-valid', 'hidden');
            spanElement.classList.add('field-validation-error')
            spanElement.innerHTML = errorMessage
        }
    }

    const textValidator = (element, minLength = 2) => {
        if (element.name === "AddressInfo.Addressline_2") {
            return;
        }

        const value = element.value.trim();
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired
            valid = false;
        } else if (value.length < minLength) {
            errorMessage = `At least ${minLength} characters is required`
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    }

    const textareaValidator = (element, minLength = 10) => {
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false
        } else if (value.length < minLength) {
            errorMessage = `At least ${minLength} characters are required`
            valid = false
        }

        formErrorHandler(element, valid, errorMessage)
    }

    const emailValidator = (element) => {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired
            valid = false
        } else if (!regex.test(value)) {
            errorMessage = 'Your email address is invalid'
            valid = false
        }

        formErrorHandler(element, valid, errorMessage);
    }

    const passwordValidator = (element) => {
        const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired
            valid = false
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid password'
            valid = false
        }

        formErrorHandler(element, valid, errorMessage);
    }

    const phonenumberValidator = (element) => {
        const regex = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false
        } else if (!regex.test(value)) {
            errorMessage = 'Your phonenumber is invalid'
            valid = false
        }

        formErrorHandler(element, valid, errorMessage);
    }

    const postalcodeValidator = (element) => {
        const regex = /^[0-9a-zA-Z\s]{3,10}$/
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired
            valid = false
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid postal code'
            valid = false
        }

        formErrorHandler(element, valid, errorMessage);
    }

    const numberValidator = (element) => {
        const value = element.value.trim();
        let errorMessage = '';
        let valid = true;

        if (value.length === 0 && !element.name.includes("DiscountPrice")) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (isNaN(value.replace(',', '.'))) {
            errorMessage = 'Enter a valid number';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const checkboxValidator = (element) => {
        if (element.checked) {
            formErrorHandler(element, true)
        }
        else {
            formErrorHandler(element, false)

        }
    }

    const selectValidator = (element) => {
        const value = element.value.trim()
        let errorMessage = ''
        let valid = true

        if (value === '') {
            errorMessage = 'Service selection is required'
            valid = false
        }

        formErrorHandler(element, valid, errorMessage);
    }

    let forms = document.querySelectorAll('form')

    forms.forEach(form => {
        let inputs = form.querySelectorAll('input, textarea, select')

        inputs.forEach(input => {
            if (input.dataset.val === 'true') {

                if (input.type === 'checkbox') {
                    input.addEventListener('change', (e) => {
                        checkboxValidator(e.target)
                    })
                }
                else if (input.type === 'select-one') {
                    input.addEventListener('change', (e) => {
                        selectValidator(e.target)
                    })
                }
                else {
                    input.addEventListener('keyup', (e) => {

                        if (input.name.includes("PostalCode")) {
                            postalcodeValidator(e.target);
                        } else if (input.name.includes("PhoneNumber")) {
                            phonenumberValidator(e.target);
                        } else if (input.name.includes("Price") || input.name.includes("DiscountPrice") || input.name.includes("Hours") || input.name.includes("LikesInProcent") || input.name.includes("LikesInNumbers")) {
                            numberValidator(e.target);
                        } else if (input.type === 'email') {
                            emailValidator(e.target);
                        } else if (input.type === 'password') {
                            passwordValidator(e.target);
                        } else if (input.type === 'textarea') {
                            textareaValidator(e.target);
                        } else {
                            textValidator(e.target);
                        }
                    })
                }
            }
        })
    })

    const cancelSecurityButton = document.getElementById('cancelSecurity')

    if (cancelSecurityButton) {
        cancelSecurityButton.addEventListener('click', function () {
            resetForm('#securityForm')
        })
    } else {
        console.error("Cancel button not found");
    }

    document.getElementById('cancelBasicDetails').addEventListener('click', function () {
        resetForm('#basicDetailsForm')
    })
    document.getElementById('cancelAddressDetails').addEventListener('click', function () {
        resetForm('#addressDetailsForm')
    })

    function resetForm(formSelector) {
        const form = document.querySelector(formSelector)
        form.reset()

        const errorElements = form.querySelectorAll('.input-validation-error, .field-validation-error, .field-validation-valid')
        errorElements.forEach(element => {
            element.classList.remove('input-validation-error', 'field-validation-error', 'field-validation-valid')
            if (element.tagName.toLowerCase() === 'span') {
                element.textContent = '';
            }
        })
    }
})
