function formErrorHandler(targetElement, validationResult)
{
    let spanElement = document.querySelector(`[data-valmsg-for="${targetElement.name}"]`);

    if (validationResult) {
        targetElement.classList.remove('input-validation-error');
        spanElement.classList.add("checkmark")
        spanElement.classList.remove('field-validation-error');
        spanElement.classList.add('field-validation-valid');
        spanElement.innerHTML = "";
    } else {
        targetElement.classList.add('input-validation-error');
        spanElement.classList.add('field-validation-error');
        spanElement.classList.remove("checkmark")
        spanElement.classList.remove('field-validation-valid');
        spanElement.innerHTML = targetElement.dataset.valRequired;
    }
}

function emailValidator(targetElement) {
    const emailRegex = /^[^\s@]{2,}@[^@\s]{2,}\.[^\s@]{2,}$/;
    formErrorHandler(targetElement, emailRegex.test(targetElement.value));
}

function passwordValidator(targetElement) {
    const passwordRegex = /^(?=.*[A-Z])(?=.*[\W_])(?=.{8,})/;
    formErrorHandler(targetElement, passwordRegex.test(targetElement.value));
}

function confirmPasswordValidator(targetElement)
{
    if (targetElement.value === document.getElementById("password-signUp").value) {
        formErrorHandler(targetElement, true);
    } else {
        formErrorHandler(targetElement, false);
    }
}

function textValidator(targetElement, minLength = 2) {
    formErrorHandler(targetElement, targetElement.value.length >= minLength);
}

function checkboxValidator(targetElement)
{
    const noSuccess = document.getElementById("termsspan")
    noSuccess.classList.add("hide")
    let spanElement = document.querySelector(`[data-valmsg-for="${targetElement.name}"]`);
    targetElement.addEventListener("click", function (event)
    {
        const target = event.target;

        if (target.checked) {
            noSuccess.classList.add("hide")
            spanElement.classList.remove('field-validation-error');
        }
        else
        {
            noSuccess.classList.remove("hide")
            spanElement.classList.add('field-validation-error');
        }
    })


    formErrorHandler(targetElement, targetElement.checked);
}

let forms = document.querySelectorAll('form');
let inputs = forms[0].querySelectorAll('input');

inputs.forEach(input => {
    if (input.dataset.val === 'true') {
        if (input.type === 'checkbox') {
            input.addEventListener('change', (e) => {
                checkboxValidator(e.target);
            });
        } else {
            input.addEventListener('keyup', (e) => {
                switch (e.target.type) {
                    case 'text':
                        textValidator(e.target);
                        break;
                    case 'email':
                        emailValidator(e.target);
                        break;
                    case 'password':
                        passwordValidator(e.target);
                        break;
                    case 'confirmPassword':
                        confirmPasswordValidator(e.target);
                        break;
                }
            });
        }
    }
});

