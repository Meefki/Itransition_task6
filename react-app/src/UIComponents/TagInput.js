import React, { useContext, useEffect, useState } from "react";
import { Dropdown } from "antd";
import TagsContext from "../Contexts/FilterTagsContext";

function TagInput({ tags, addTag, inputPlaceholder }) {

    const { getTags } = useContext(TagsContext);

    const [searchedText, setSearchedText] = useState('');

    function menuItemClick(key) {
        const keyText = key.target.text;
        setSearchedText('');
        getTags('');
        addTag(keyText);
    }

    const menuItems = () => {
        let tagItems = [...tags];
        if (searchedText && !tags.find(tag => tag === searchedText)) tagItems.unshift(searchedText)
        const menuItems = tagItems.map((tag) => {
            return { key: tag, label: (
            <a key={tag} onClick={menuItemClick} >{tag}</a>
        )}});

        return menuItems;
    }

    return (
        <Dropdown menu={{ items: menuItems() }} trigger={['click']}>
            <input
                size="large"
                value={searchedText}
                className="m-1 input-group-text"
                placeholder={inputPlaceholder}
                onChange={(e) => {
                    setSearchedText(e.target.value);
                    getTags(e.target.value);
                }}
            />
        </Dropdown>
    );
}

export default TagInput;