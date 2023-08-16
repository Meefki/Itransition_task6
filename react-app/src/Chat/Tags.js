import React, { useContext, useState } from "react";
import { Tag } from 'antd';
import TagInput from "../UIComponents/TagInput";

const styles = {
    a: {
        cursor: 'default',
        userSelect: 'none'
    }
}

function Tags({ tags, selectedTags, setSelectedTags, inputPlaceholder }) {

    function addTag(key) {
        if (!selectedTags.find(tagKey => tagKey === key))
        {
            let selTags = [...selectedTags];
            selTags.push(key);
            selTags.sort();
            setSelectedTags(selTags);
        }
    }

    function removeTag(key) {
        const selTags = selectedTags.filter(t => t !== key.tag);
        setSelectedTags(selTags);
    }

    function selectTags() {
        return tags.filter(t => !selectedTags.includes(t));
    }

    function copyTags() {
        const selTags = selectedTags.concat(tags.filter(t => !selectedTags.includes(t)));
        selTags.sort()
        setSelectedTags(selTags);
    }

    return (
        <div className="hafl-height-container border-end shadow">
            <div className="bg-light border-bottom shadow-sm mb-3"  style={{ minHeight: 51 }}>
                <div className="container-fluid">
                    <div className="d-flex justify-content-between">
                        <TagInput tags={selectTags()} addTag={addTag} inputPlaceholder={inputPlaceholder}/>
                        <button className="btn btn-primary m-1" onClick={() => copyTags()} title="Copying all existing tags to area of selected tags">Copy</button>
                    </div>
                </div>
            </div>
            
            <div>
                {
                    selectedTags.map((tag, index) => 
                    <a
                        style={styles.a}
                        className="text-decoration-none"
                        key={tag}
                        onClick={() => removeTag({tag})}>
                            <Tag className="m-1" color="#55acee">
                                {tag}
                            </Tag>
                    </a>)
                }
            </div>
        </div>
    )
}

export default Tags;