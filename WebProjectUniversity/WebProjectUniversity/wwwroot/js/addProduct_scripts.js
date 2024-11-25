function setupColorHandlers() {
    const customColorCheckbox = document.getElementById('customColorCheckbox');
    const customColorContainer = document.getElementById('custom-color-container');
    const addMoreLink = document.getElementById('add-more-custom-colors');
    const hiddenColorInput = document.getElementById('HiddenColorsInput');

    // Initially show/hide custom color inputs based on checkbox state
    toggleCustomColorInputs(customColorCheckbox.checked);

    // Event listener for "Custom Color" checkbox
    customColorCheckbox.addEventListener('change', function () {
        toggleCustomColorInputs(this.checked);
        updateColorValues(); // Update color values when the checkbox state changes
    });

    // Event listener for adding more custom color inputs
    addMoreLink.addEventListener('click', function () {
        addCustomColorInput();
    });

    // Update hidden color input with selected values
    function updateColorValues() {
        const colorValues = new Set();
        const customColorInputs = document.querySelectorAll('.custom-color-container input');
        const checkboxInputs = document.querySelectorAll('input[name="Colors"][type="checkbox"]:checked');

        // Remove empty custom color inputs before updating values
        customColorInputs.forEach(function (input) {
            const colorValue = input.value.trim().toLowerCase();
            if (colorValue !== '') {
                colorValues.add(colorValue);
            } else {
                // Remove empty custom color input field
                input.closest('.custom-color-container').remove();
            }
        });

        checkboxInputs.forEach(function (input) {
            colorValues.add(input.value.trim().toLowerCase());
        });

        hiddenColorInput.value = Array.from(colorValues).filter(Boolean).join(','); // Filter out empty values
    }

    // Show/hide custom color input fields based on checkbox state
    function toggleCustomColorInputs(show) {
        const customColorInputs = customColorContainer.querySelectorAll('.custom-color-container');
        customColorInputs.forEach(function (inputContainer) {
            inputContainer.style.display = show ? 'block' : 'none';
        });
        addMoreLink.style.display = show ? 'inline' : 'none';
    }

    // Add new custom color input field
    function addCustomColorInput(value = '') {
        const inputTemplate = document.createElement('div');
        inputTemplate.className = 'custom-color-container';
        inputTemplate.style.display = 'block';
        inputTemplate.innerHTML = `
            <input type="text" style="width:160px;" class="form-input" name="Colors" value="${value}" placeholder="Enter custom color">
            <button type="button" class="remove-custom-color-container" style="display: inline;">Remove</button>
        `;

        const removeButton = inputTemplate.querySelector('.remove-custom-color-container');
        removeButton.addEventListener('click', function () {
            inputTemplate.remove();
            updateColorValues(); // Update color values when removed
        });

        const input = inputTemplate.querySelector('input');
        input.addEventListener('input', function () {
            checkForDuplicates(this);
        });

        customColorContainer.insertBefore(inputTemplate, addMoreLink); // Add new input before the "Add More?" link
    }

    // Check for duplicate custom color values
    function checkForDuplicates(input) {
        const currentValue = input.value.trim().toLowerCase();
        if (currentValue === '') return;

        const customColorInputs = document.querySelectorAll('.custom-color-container input');
        const checkboxInputs = document.querySelectorAll('input[name="Colors"][type="checkbox"]:checked');
        let duplicateFound = false;

        // Check custom color inputs for duplicates
        customColorInputs.forEach(function (otherInput) {
            if (otherInput !== input && otherInput.value.trim().toLowerCase() === currentValue) {
                duplicateFound = true;
            }
        });

        // Check checkbox inputs for duplicates
        checkboxInputs.forEach(function (checkbox) {
            if (checkbox.value.trim().toLowerCase() === currentValue) {
                duplicateFound = true;
            }
        });

        // If duplicate found, clear the input and show an alert
        if (duplicateFound) {
            input.value = ''; // Clear the input field
            alert(`Duplicate color value "${currentValue}" detected. Please enter a unique color or select from the available colors.`);
        }

        updateColorValues(); // Update the hidden color values
    }

    // Initialize custom color inputs if they exist
    document.querySelectorAll('.custom-color-container input').forEach(function (input) {
        input.addEventListener('input', function () {
            checkForDuplicates(this);
        });
    });

    // Event listener for checkbox inputs
    document.querySelectorAll('input[name="Colors"][type="checkbox"]').forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            checkForDuplicatesInCheckboxes(this);
            updateColorValues();
        });
    });

    // Check if the selected checkbox is a duplicate (compare with custom input values)
    function checkForDuplicatesInCheckboxes(checkbox) {
        const checkboxValue = checkbox.value.trim().toLowerCase();
        const customColorInputs = document.querySelectorAll('.custom-color-container input');
        let duplicateFound = false;

        customColorInputs.forEach(function (input) {
            if (input.value.trim().toLowerCase() === checkboxValue) {
                duplicateFound = true;
            }
        });

        if (duplicateFound) {
            checkbox.checked = false; // Uncheck if it's a duplicate
            alert(`This color is already entered as a custom color. Please choose a different color.`);
        }
    }

    // Validate that custom color inputs are not blank before form submission
    function validateCustomColorInputs() {
        const customColorInputs = document.querySelectorAll('.custom-color-container input');
        let isValid = true;

        customColorInputs.forEach(function (input) {
            if (input.value.trim() === '') {
                isValid = false;
                input.style.borderColor = 'red'; // Visual feedback for empty input
                alert("Custom color cannot be blank!");
            } else {
                input.style.borderColor = ''; // Reset the border color if input is valid
            }
        });

        return isValid; // Return the validity status
    }

    // Event listener for form submission (can be attached to your form submit button)
    const form = document.querySelector('form'); // Assuming there's a <form> element
    form.addEventListener('submit', function (event) {
        if (!validateCustomColorInputs()) {
            event.preventDefault(); // Prevent form submission if validation fails
        } else {
            updateColorValues(); // Update the hidden field before submitting
        }
    });

    // Initial update to handle pre-selected values
    updateColorValues();
}




function setupProductTypes() {
    function updateProductTypes() {
        const selectedCategoryCount = $('#product-categories-container select').filter(function () {
            return $(this).val() !== ""; // Count only non-empty selections
        }).length;

        if (selectedCategoryCount > 0) {
            $('#add-more-categories').show();
            $('#category-notification').hide(); // Hide notification when categories are selected
        } else {
            $('#add-more-categories').hide();
            $('#category-notification').show(); // Show notification when no categories are selected
        }

        const selectedCategoryIds = [];
        $('#product-categories-container select').each(function () {
            const val = $(this).val();
            if (val) selectedCategoryIds.push(val);
        });

        const productTypeDropdown = $('#ProductType-dropdown');
        if (selectedCategoryIds.length > 0) {
            $.ajax({
                url: getProductTypesUrl,
                type: 'GET',
                traditional: true, // Ensure the array is sent properly
                data: { categoryIds: selectedCategoryIds },
                success: function (response) {
                    productTypeDropdown.empty(); // Clear existing options
                    productTypeDropdown.append('<option value="">Select a ProductType</option>');

                    const existingValues = new Set(); // To track existing product type values

                    response.forEach(function (item) {
                        if (!existingValues.has(item.value)) { // Check for duplicates
                            existingValues.add(item.value);
                            const option = $('<option></option>')
                                .attr('value', item.value)
                                .text(item.text);
                            productTypeDropdown.append(option);
                        }
                    });

                    // Set the selected value after options are populated
                    if (selectedProductTypeId) {
                        productTypeDropdown.val(selectedProductTypeId); // Select the product type
                    }

                    $('#ProductType-container').show(); // Show the product type container
                    productTypeDropdown.prop('disabled', false); // Enable dropdown if categories are selected
                },
                error: function (xhr, status, error) {
                    console.error("An error occurred: " + error);
                }
            });
        } else {
            productTypeDropdown.empty();
            productTypeDropdown.append('<option value="" disabled>Select a ProductType</option>');
            $('#ProductType-container').show(); // Keep the container visible
            productTypeDropdown.prop('disabled', true); // Disable until a category is selected
        }

        // Disable selected categories in other dropdowns
        const selectedValues = [];
        $('#product-categories-container select').each(function () {
            const selectedVal = $(this).val();
            if (selectedVal) {
                selectedValues.push(selectedVal);
            }
        });

        $('#product-categories-container select').each(function () {
            const currentDropdown = $(this);
            currentDropdown.find('option').prop('disabled', false); // Enable all options first
            selectedValues.forEach(function (value) {
                if (value !== currentDropdown.val()) {
                    currentDropdown.find('option[value="' + value + '"]').prop('disabled', true);
                }
            });
        });
    }

    // Initialize notification visibility on page load
    updateProductTypes(); // Call updateProductTypes to set the initial state of the notification

    // Add event listener for category dropdown change
    $('#product-categories-container').on('change', 'select', updateProductTypes);

    // Add more categories
    $('#add-more-categories').click(function () {
        const container = $('#product-categories-container');
        const selectTemplate = container.find('.product-category-select').first().clone();

        // Reset the select element's value and update the name attribute
        selectTemplate.find('select').val('');
        const newIndex = container.children().length; // Get the new index based on the number of children
        selectTemplate.find('select').attr('name', 'CategoryIds[' + newIndex + ']'); // Set name correctly

        // Ensure the remove button is properly set up
        selectTemplate.find('.remove-category').css('display', 'inline').off('click').on('click', function () {
            $(this).parent().remove();
            updateProductTypes(); // Refresh the state
        });

        container.append(selectTemplate);
        updateProductTypes(); // Ensure the new dropdown updates the state correctly
    });

    // Add event listener for the initial remove button if it's visible
    $('.remove-category').each(function () {
        $(this).click(function () {
            $(this).parent().remove();
            updateProductTypes(); // Refresh the state
        });
    });
}

let materialCount = 1; // Start from 1 for indexing
let existingKeys = new Set(); // Use a Set to track existing keys

function setupMaterials() {
    // Function to update remove buttons
    function updateRemoveButtons() {
        const materialEntries = $('#materials-container .material-entry');
        materialEntries.each(function (index) {
            const removeButton = $(this).find('.remove-material');
            if (materialEntries.length > 1 && index > 0) { // Show remove button only if there are more than 1 entries
                removeButton.show();
            } else {
                removeButton.hide(); // Hide remove button for the first entry
            }
        });
    }

    $('#add-more-materials').click(function () {
        const newMaterialEntry =
            $('<div class="material-entry">' +
                '<div>' +
                '<label for="materialName">Material</label>' +
                '<input type="text" class="form-input" name="Materials[' + materialCount + '].Key" required placeholder="Material Name" />' +
                '</div>' +
                '<div>' +
                '<label for="materialPercent">Percent</label>' +
                '<input type="number" class="form-input" name="Materials[' + materialCount + '].Value" required placeholder="Percent" min="0" max="100" step="0.01" />' +
                '<span>%</span>' +
                '</div>' +
                '<button type="button" class="remove-material">Remove</button>' +
                '</div>');

        $('#materials-container').append(newMaterialEntry);
        materialCount++; // Increment the counter for the next entry
        updateRemoveButtons(); // Update the visibility of the remove buttons
    });

    $('#materials-container').on('input', 'input[name*=".Key"]', function () {
        const inputField = $(this);
        const currentKey = inputField.val().trim();

        // Remove existing key from the set for re-checking
        if (existingKeys.has(inputField.data('originalKey'))) {
            existingKeys.delete(inputField.data('originalKey'));
        }

        // Check for duplicates
        if (existingKeys.has(currentKey)) {
            alert(`The material "${currentKey}" already exists. Please enter a unique material name.`);
            inputField.val(''); // Clear the input field to prevent duplicate entry
        } else {
            // Update the original key in the set
            inputField.data('originalKey', currentKey); // Store the original key for future checks
            existingKeys.add(currentKey); // Add new key to the set
        }
    });

    $('#materials-container').on('click', '.remove-material', function () {
        const keyInput = $(this).closest('.material-entry').find('input[name*=".Key"]');
        const keyToRemove = keyInput.val().trim();
        existingKeys.delete(keyToRemove); // Remove the key from the Set
        $(this).closest('.material-entry').remove();
        updateRemoveButtons(); // Update the visibility of the remove buttons
    });

    // Initial call to set the correct state of remove buttons
    updateRemoveButtons();
}

function setupBrandInputHandling() {
    const brandDropdown = document.getElementById("brand-dropdown");
    const brandInput = document.getElementById("brand-input");

    // Function to validate and manage requirement logic
    function validateBrandFields() {
        if (brandDropdown.value) {
            brandInput.value = "";
            brandInput.disabled = true;
            brandDropdown.removeAttribute('required');
        } else if (brandInput.value) {
            brandDropdown.value = "";
            brandDropdown.disabled = true;
            brandInput.setAttribute('required', 'required');
        } else {
            brandInput.disabled = false;
            brandDropdown.disabled = false;
            brandInput.removeAttribute('required');
        }
    }

    // Initial validation
    validateBrandFields();

    // Event listener for the dropdown
    brandDropdown.addEventListener("change", function () {
        validateBrandFields();
    });

    // Event listener for the input field
    brandInput.addEventListener("input", function () {
        validateBrandFields();
    });
}
function setupLengthInputHandling() {
    const lengthsDropdown = document.getElementById("lengths-dropdown");
    const lengthsInput = document.getElementById("lengths-input");

    function validateLengthFields() {
        if (lengthsDropdown.value) {
            lengthsInput.value = "";
            lengthsInput.disabled = true;
            lengthsDropdown.removeAttribute('required');
        } else if (lengthsInput.value) {
            lengthsDropdown.value = "";
            lengthsDropdown.disabled = true;
            lengthsInput.setAttribute('required', 'required');
        } else {
            lengthsInput.disabled = false;
            lengthsDropdown.disabled = false;
            lengthsInput.removeAttribute('required');
        }
    }

    validateLengthFields();

    lengthsDropdown.addEventListener("change", validateLengthFields);
    lengthsInput.addEventListener("input", validateLengthFields);
}

function setupStylesHandling() {
    const stylesDropdown = document.getElementById("styles-dropdown");
    const stylesInput = document.getElementById("styles-input");
    const addStyleButtons = document.querySelectorAll('.add-style');
    const addedStylesContainer = document.querySelector('.added-styles');
    const stylesHiddenInput = document.getElementById("styles-hidden");

    let addedStyles = []; // Array to hold the added styles

    // Function to update the hidden input with the current styles
    function updateHiddenInput() {
        // Clear previous hidden inputs
        const existingInputs = document.querySelectorAll('input[name="Styles[]"]');
        existingInputs.forEach(input => input.remove()); // Remove old hidden inputs

        // Populate it with the current added styles
        addedStyles.forEach(style => {
            const hiddenInput = document.createElement('input');
            hiddenInput.type = 'hidden';
            hiddenInput.name = 'Styles[]'; // Ensure this is 'Styles[]' for model binding
            hiddenInput.value = style; // Set the value for the hidden input
            // Append the hidden input to the form
            document.forms[0].appendChild(hiddenInput);
        });
    }

    function addStyle(style) {
        // Convert style to lowercase for case-insensitive comparison
        const lowerCaseStyle = style.toLowerCase();

        // Check if the style is valid, not empty, and not already added (case-insensitive)
        if (style && !addedStyles.some(s => s.toLowerCase() === lowerCaseStyle)) {
            addedStyles.push(style); // Add the style to the array

            // Create a new element to display the added style
            const styleElement = document.createElement('div');
            styleElement.textContent = style; // Display the style text

            // Create a remove button
            const removeButton = document.createElement('button');
            removeButton.textContent = 'Remove';
            removeButton.type = 'button';
            removeButton.addEventListener('click', () => {
                addedStyles = addedStyles.filter(s => s.toLowerCase() !== lowerCaseStyle); // Remove the style from the array
                styleElement.remove(); // Remove the displayed style element
                updateHiddenInput(); // Update the hidden input value
            });

            // Append the remove button to the style element
            styleElement.appendChild(removeButton);
            addedStylesContainer.appendChild(styleElement); // Add the style element to the container
            updateHiddenInput(); // Update hidden input value with the current styles
        } else if (addedStyles.some(s => s.toLowerCase() === lowerCaseStyle)) {
            alert('Style already added!'); // Alert for duplicates
        } else {
            alert('Cannot add an empty style!'); // Alert for empty input
        }
    }

    // Set up event listeners for the add buttons
    addStyleButtons.forEach(button => {
        button.addEventListener('click', () => {
            const dropdownValue = stylesDropdown.value;
            const inputValue = stylesInput.value.trim();

            // Check dropdown value
            if (dropdownValue) {
                addStyle(dropdownValue); // Use the dropdown value
                stylesDropdown.value = ''; // Clear the dropdown selection
            } else if (inputValue) {
                addStyle(inputValue); // Use the input value
                stylesInput.value = ''; // Clear the input value
            } else {
                alert('Please select a style from the dropdown or enter a style.'); // Alert if neither is selected
            }
        });
    });
}


// Initialize all setup functions
function initialize() {
    setupColorHandlers();
    setupProductTypes();
    setupMaterials();
    setupBrandInputHandling();
    setupLengthInputHandling();
    setupStylesHandling();

}
// Call initialize when the document is ready
$(document).ready(initialize);
